using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LightingAndCamerasExample
{
	/// <summary>
	/// A camera controlled by WASD + Mouse
	/// </summary>
	public class FPSCamera : ICamera
	{
		/// <summary>
		/// The angle of rotation about the Y-axis
		/// </summary>
		private float _horizontalAngle;

		/// <summary>
		/// The angle of rotation about the X-axis
		/// </summary>
		private float _verticalAngle;

		/// <summary>
		/// The camera's position in the world
		/// </summary>
		private Vector3 _position;

		/// <summary>
		/// The state of the mouse in the prior frame
		/// </summary>
		private MouseState _oldMouseState;

		/// <summary>
		/// The direction the camera is facing
		/// </summary>
		private Vector3 _direction;

		/// <summary>
		/// The Game this camera belongs to
		/// </summary>
		private Game _game;

		/// <summary>
		/// The view matrix for this camera
		/// </summary>
		public Matrix View { get; protected set; }

		/// <summary>
		/// The projection matrix for this camera
		/// </summary>
		public Matrix Projection { get; protected set; }

		/// <summary>
		/// The sensitivity of the mouse when aiming
		/// </summary>
		public float Sensitivity { get; set; } = 0.0018f;

		/// <summary>
		/// The speed of the player while moving
		/// </summary>
		public float Speed { get; set; } = 0.5f;

		/// <summary>
		/// Constructs a new FPS Camera
		/// </summary>
		/// <param name="game">The game this camera belongs to</param>
		/// <param name="position">The player's initial position</param>
		public FPSCamera(Game game, Vector3 position)
		{
			_game = game;
			_position = position;
			_horizontalAngle = 0;
			_verticalAngle = 0;
			Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
				game.GraphicsDevice.Viewport.AspectRatio, 1, 1000);
			Mouse.SetPosition(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height / 2);
			_oldMouseState = Mouse.GetState();
		}

		/// <summary>
		/// Updates the camera
		/// </summary>
		/// <param name="gameTime">The current GameTime</param>
		public void Update(GameTime gameTime)
		{
			KeyboardState keyboard = Keyboard.GetState();
			MouseState newMouseState = Mouse.GetState();

			Vector3 facing = Vector3.Transform(Vector3.Forward, Matrix.CreateRotationY(_horizontalAngle));

			// Forward and backward movement
			if (keyboard.IsKeyDown(Keys.W)) _position += facing * Speed;
			if (keyboard.IsKeyDown(Keys.S)) _position -= facing * Speed;

			// Strafing movement
			if (keyboard.IsKeyDown(Keys.A)) _position += Vector3.Cross(Vector3.Up, facing) * Speed;
			if (keyboard.IsKeyDown(Keys.D)) _position -= Vector3.Cross(Vector3.Up, facing) * Speed;

			// Adjust horizontal angle
			_horizontalAngle += Sensitivity * (_oldMouseState.X - newMouseState.X);

			// Adjust vertical angle
			_verticalAngle += Sensitivity * (_oldMouseState.Y - newMouseState.Y);

			_direction = Vector3.Transform(Vector3.Forward, Matrix.CreateRotationX(_verticalAngle) * Matrix.CreateRotationY(_horizontalAngle));

			// Create the view matrix
			View = Matrix.CreateLookAt(_position, _position + _direction, Vector3.Up);

			// Reset mouse state
			Mouse.SetPosition(_game.Window.ClientBounds.Width / 2, _game.Window.ClientBounds.Height / 2);
			_oldMouseState = Mouse.GetState();
		}
	}
}
