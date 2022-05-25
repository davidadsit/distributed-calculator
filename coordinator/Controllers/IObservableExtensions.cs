using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace coordinator.Controllers;

public static class IObservableExtensions
{
    public static Task<ActionResult> ToActionResult<T>(this IObservable<T> source, Func<T, ActionResult> successAction)
    {
        return ToActionResult(source, null, successAction, null, null);
    }

    public static Task<ActionResult> ToActionResult<T>(this IObservable<T> source, Func<T, ActionResult> successAction, TimeSpan timeout)
    {
        return ToActionResult(source, null, successAction, null, timeout);
    }

    public static Task<ActionResult> ToActionResult<T>(this IObservable<T> source, IScheduler scheduler, Func<T, ActionResult> successAction, TimeSpan timeout)
    {
        return ToActionResult(source, scheduler, successAction, null, timeout);
    }

    public static Task<ActionResult> ToActionResult<T>(this IObservable<T> source, IScheduler scheduler, Func<T, ActionResult> successAction, Func<ActionResult> failAction = null)
    {
        return ToActionResult(source, scheduler, successAction, failAction, null);
    }

    public static Task<ActionResult> ToActionResult<T>(
        this IObservable<T> source,
        IScheduler scheduler,
        Func<T, ActionResult> successAction,
        Func<ActionResult> failAction, TimeSpan? timeout)
    {
        timeout = timeout ?? TimeSpan.FromSeconds(30);
        if (scheduler == null)
        {
            scheduler = new EventLoopScheduler();
        }

        return source
            .Take(1)
            .Select(successAction)
            .Timeout(timeout.Value, scheduler)
            .Catch<ActionResult, Exception>(e => Observable.Return(failAction != null ? failAction() : new NotFoundResult()))
            .ToTask();
    }

}