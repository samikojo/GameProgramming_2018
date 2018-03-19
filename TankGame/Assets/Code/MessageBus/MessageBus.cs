using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TankGame.Messaging
{
	public class MessageBus : IMessageBus
	{
		private readonly Dictionary< Type, IList > _subscriptions
			= new Dictionary< Type, IList >();

		public void Publish< TMessage >( TMessage message )
			where TMessage : IMessage
		{
			if ( message == null )
			{
				throw new ArgumentNullException( "message" );
			}

			Type messageType = typeof( TMessage );
			if ( _subscriptions.ContainsKey( messageType ) )
			{
				var subscriptionsList = new List< ISubscription< TMessage > >(
						_subscriptions[ messageType ].Cast< ISubscription< TMessage > >() );
				foreach ( var subscription in subscriptionsList )
				{
					subscription.Action( message );
				}
			}
		}

		public ISubscription< TMessage >
			Subscribe< TMessage >( Action< TMessage > action )
			where TMessage : IMessage
		{
			Type messageType = typeof( TMessage );
			Subscription< TMessage > subscription =
				new Subscription< TMessage >( this, action );

			if ( _subscriptions.ContainsKey( messageType ) )
			{
				_subscriptions[ messageType ].Add( subscription );
			}
			else
			{
				_subscriptions.Add( messageType,
					new List<ISubscription<TMessage>>() { subscription } );
			}

			return subscription;
		}

		public void Unsubscribe< TMessage >( ISubscription< TMessage > subscription )
			where TMessage : IMessage
		{
			Type messageType = typeof( TMessage );
			if ( _subscriptions.ContainsKey( messageType ) )
			{
				_subscriptions[messageType].Remove( subscription );
			}
		}

		public void Clear()
		{
			_subscriptions.Clear();
		}
	}
}
