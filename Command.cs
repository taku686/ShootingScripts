namespace InputCommand
{
	/// <summary>
	/// プレイヤー用コマンドのインタフェース
	/// </summary>
	public interface IPlayerCommand
	{
		void Execute(Player player);
	}

	/// <summary>
	/// ジャンプ
	/// </summary>
	public class MoveCommand : IPlayerCommand
	{
		void IPlayerCommand.Execute(Player player)
		{
			player.Move();
		}
	}

	/// <summary>
	/// 発射コマンド
	/// </summary>
	public class FireCommand : IPlayerCommand
	{
		void IPlayerCommand.Execute(Player player)
		{
			player.Fire();
		}
	}
}
