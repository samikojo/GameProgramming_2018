using System;

namespace TankGame.Messaging
{
	/// <summary>
	/// Defines the public interface of any message bus we implement to our
	/// system.
	/// </summary>
	public interface IMessageBus
	{
		/// <summary>
		/// Publishes the message to all subscribers.
		/// </summary>
		/// <typeparam name="TMessage">The type of the message.</typeparam>
		/// <param name="message">The message being published.</param>
		void Publish< TMessage >( TMessage message )
			where TMessage : IMessage;

		/// <summary>
		/// Subscribes to receive messages of type TMessage.
		/// </summary>
		/// <typeparam name="TMessage">The type of the message</typeparam>
		/// <param name="action">The action which is executed when
		/// the message is published.</param>
		/// <returns>A reference to the subscription.</returns>
		ISubscription< TMessage >
			Subscribe< TMessage >( Action< TMessage > action )
			where TMessage : IMessage;

		/// <summary>
		/// Unsubscribes from receiving messages of type TMessage.
		/// </summary>
		/// <typeparam name="TMessage">The type of the message.</typeparam>
		/// <param name="subscription">The subscription we are
		/// unsubscribing from.</param>
		void Unsubscribe< TMessage >( ISubscription< TMessage > subscription )
			where TMessage : IMessage;

		/// <summary>
		/// Removes all subscriptions from the MessageBus.
		/// </summary>
		void Clear();
	}
}
