using System.Collections.Generic;
using UnityEngine;

namespace InputCommand
{
	public class Player : MonoBehaviour
	{
		public enum Action
		{
			// 移動
			Move,
			// 弾を発射
			Fire
		}

		[Header("移動速度")]
		[SerializeField] private float speed;
		[Header("弾プレハブ")]
		[SerializeField] private GameObject bulletPrefab;
		[Header("弾の初速")]
		[SerializeField] private float bulletForce;
		[Header("Zキーのアクション")]
		[SerializeField] private Action actionKeySpace = Action.Fire;
		[Header("Xキーのアクション")]
		[SerializeField] private Action actionKeyAxis = Action.Move;
		[SerializeField] private Transform _shotPosition;
		private Rigidbody _rigidbody;
		

		private IPlayerCommand commandKeySpace;
		private IPlayerCommand commandKeyAxis;



		// Queue = 先入れ先出しリスト
		private Queue<IPlayerCommand> commandQueue = new Queue<IPlayerCommand>();

		public void EnqueueCommand(IPlayerCommand command)
		{
			commandQueue.Enqueue(command);
		}

		private void Start()
		{
			commandKeySpace = GenerateCommand(actionKeySpace);
			commandKeyAxis = GenerateCommand(actionKeyAxis);
			_rigidbody = GetComponent<Rigidbody>();
		}

		
		public IPlayerCommand GenerateCommand(Action action)
		{
			switch (action)
			{
				case Action.Move:
					return new MoveCommand();

				case Action.Fire:
					return new FireCommand();
			}
			return null;
		}

		public void Move()
		{
			float x = Input.GetAxis("Horizontal") * speed;
			float y = Input.GetAxis("Vertical") * speed;
			_rigidbody.velocity = new Vector3(x, y, 0);
		}

		public void Fire()
		{
			var bullet = Instantiate(bulletPrefab,_shotPosition);
			var rigidbody = bullet.GetComponent<Rigidbody>();
			rigidbody.AddForce(Vector3.up * bulletForce, ForceMode.Impulse);
		}

		private void HundleInput()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				EnqueueCommand(commandKeySpace);
			}
			if (Input.GetKey(KeyCode.UpArrow)
				|| Input.GetKey(KeyCode.RightArrow) 
				|| Input.GetKey(KeyCode.DownArrow) 
				|| Input.GetKey(KeyCode.LeftArrow)
				)
			{
				Move();
			}
            else
            {
				_rigidbody.velocity = Vector3.zero;
            }
		}

		private void Update()
		{
			HundleInput();
			if (commandQueue.Count == 0)
			{
				return;
			}
			IPlayerCommand command = commandQueue.Dequeue();
			command.Execute(this);
		}
	}
}
