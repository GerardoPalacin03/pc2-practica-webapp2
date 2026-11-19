using PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;

namespace PC28084.U20211c201.Prueba.Antamina.Platform.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}