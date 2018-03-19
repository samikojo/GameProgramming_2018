using System;

namespace TankGame.Messaging
{
	public interface ISubscription<TMessage> : IDisposable
		where TMessage : IMessage
	{
		/// <summary>
		/// The action which is executed when the message of type
		/// TMessage is published.
		/// </summary>
		Action<TMessage> Action { get; }

		/// <summary>
		/// The MessageBus this subscription belongs to.
		/// </summary>
		IMessageBus MessageBus { get; }
	}
}
