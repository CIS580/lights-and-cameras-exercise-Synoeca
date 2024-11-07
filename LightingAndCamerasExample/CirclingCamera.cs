using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace LightingAndCamerasExample
{
	/// <summary>
	/// A camera that circles the origin
	/// </summary>
	public class CirclingCamera : ICamera
	{
		/// <summary>
		/// The camera's angle
		/// </summary>
		private float _angle;

		/// <summary>
		/// The camera's position
		/// </summary>
		private Vector3 _postion;

		/// <summary>
		/// The camera's speed
		/// </summary>
		private float _speed;

		/// <summary>
		/// The game this camera belongs to
		/// </summary>
		private Game _game;

		/// <summary>
		/// The view matrix
		/// </summary>
		private Matrix _view;

		/// <summary>
		/// The projection matrix
		/// </summary>
		private Matrix _projection;

		/// <summary>
		/// The camera's view matrix
		/// </summary>
		public Matrix View => _view;

		/// <summary>
		/// The camera's projection matrix
		/// </summary>
		public Matrix Projection => _projection;

		/// <summary>
		/// Constructs a new camera that circles the origin
		/// </summary>
		/// <param name="game">The game this camera belongs to</param>
		/// <param name="position">The initial position of the camera</param>
		/// <param name="speed">The speed of the camera</param>
		public CirclingCamera(Game game, Vector3 position, float speed)
		{
			_game = game;
			_postion = position;
			_speed = speed;
			_projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,
				_game.GraphicsDevice.Viewport.AspectRatio,
				1,
				1000
			);
			_view = Matrix.CreateLookAt(
				_postion,
				Vector3.Zero,
				Vector3.Up
			);
		}

		/// <summary>
		/// Updates the camera's position
		/// </summary>
		/// <param name="gameTime">The GameTime object</param>
		public void Update(GameTime gameTime)
		{
			// update the angle based on the elapsed time and speed
			_angle += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

			// Calculate a new view matrix
			_view =
				Matrix.CreateRotationY(_angle) *
				Matrix.CreateLookAt(_postion, Vector3.Zero, Vector3.Up);
		}
	}
}
