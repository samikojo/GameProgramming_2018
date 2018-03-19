namespace TankGame.Messaging
{
	public class UnitGotHit : IMessage
	{
		public Unit DamagedUnit { get; private set; }

		public UnitGotHit( Unit unit )
		{
			DamagedUnit = unit;
		}
	}
}
