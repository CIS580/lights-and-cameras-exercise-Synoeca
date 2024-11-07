using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LightingAndCamerasExample
{
	public class Crate
	{
		/// <summary>
		/// The game this crate belongs to
		/// </summary>
		private Game _game;

		/// <summary>
		/// The VertexBuffer of crate vertices
		/// </summary>
		private VertexBuffer _vertexBuffer;

		/// <summary>
		/// The IndexBuffer defining the Crate's triangles
		/// </summary>
		private IndexBuffer _indexBuffer;

		/// <summary>
		/// The effect to render the crate with
		/// </summary>
		private BasicEffect _effect;

		/// <summary>
		/// The texture to apply to the crate
		/// </summary>
		private Texture2D _texture;

		/// <summary>
		/// Creates a new crate instance
		/// </summary>
		/// <param name="game">The game this crate belongs to</param>
		/// <param name="type">The type of crate to use</param>
		/// <param name="world">The position and orientation of the crate in the world</param>
		public Crate(Game game, CrateType type, Matrix world)
		{
			_game = game;
			_texture = game.Content.Load<Texture2D>($"crate{(int)type}_diffuse");
			InitializeVertices();
			InitializeIndices();
			InitializeEffect();
			_effect.World = world;
		}

		public void InitializeVertices()
		{
			VertexPositionNormalTexture[] vertexData = {
				new() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
				new() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
				new() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },
				new() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Forward },

	            // Back Face
	            new() { Position = new Vector3(-1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Backward },
				new() { Position = new Vector3( 1.0f, -1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
				new() { Position = new Vector3( 1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
				new() { Position = new Vector3(-1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },

	            // Top Face
	            new() { Position = new Vector3(-1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Up },
				new() { Position = new Vector3(-1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Up },
				new() { Position = new Vector3( 1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Up },
				new() { Position = new Vector3( 1.0f, 1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Up },

	            // Bottom Face
	            new() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Down },
				new() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Down },
				new() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Down },
				new() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Down },

	            // Left Face
	            new() { Position = new Vector3(-1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Left },
				new() { Position = new Vector3(-1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Left },
				new() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Left },
				new() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Left },

	            // Right Face
	            new() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Right },
				new() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Right },
				new() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Right },
				new() { Position = new Vector3( 1.0f, -1.0f,  1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Right },
			};
			_vertexBuffer = new VertexBuffer(_game.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
			_vertexBuffer.SetData(vertexData);
		}

		/// <summary>
		/// Initializes the Index Buffer
		/// </summary>
		public void InitializeIndices()
		{
			short[] indexData = {
				// Front face
				0, 2, 1,
				0, 3, 2,

				// Back face 
				4, 6, 5,
				4, 7, 6,

				// Top face
				8, 10, 9,
				8, 11, 10,

				// Bottom face 
				12, 14, 13,
				12, 15, 14,

				// Left face 
				16, 18, 17,
				16, 19, 18,

				// Right face 
				20, 22, 21,
				20, 23, 22
			};
			_indexBuffer = new IndexBuffer(_game.GraphicsDevice, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
			_indexBuffer.SetData(indexData);
		}

		/// <summary>
		/// Initializes the BasicEffect to render our crate
		/// </summary>
		private void InitializeEffect()
		{
			_effect = new BasicEffect(_game.GraphicsDevice);
			_effect.World = Matrix.CreateScale(2.0f);
			_effect.View = Matrix.CreateLookAt(
				new Vector3(8, 9, 12), // The camera position
				new Vector3(0, 0, 0), // The camera target,
				Vector3.Up            // The camera up vector
			);
			_effect.Projection = Matrix.CreatePerspectiveFieldOfView(
				MathHelper.PiOver4,                         // The field-of-view 
				_game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
				0.1f, // The near plane distance 
				100.0f // The far plane distance
			);
			_effect.TextureEnabled = true;
			_effect.Texture = _texture;

			// Turn on lighting
			_effect.LightingEnabled = true;
			// Set up light 0
			_effect.DirectionalLight0.Enabled = true;
			_effect.DirectionalLight0.Direction = new Vector3(1f, 0, 1f);
			_effect.DirectionalLight0.DiffuseColor = new Vector3(0.8f, 0, 0);
			_effect.DirectionalLight0.SpecularColor = new Vector3(1f, 0.4f, 0.4f);
			_effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);
		}

		/// <summary>
		/// Draws the crate
		/// </summary>
		public void Draw(ICamera camera)
		{
			// set the view and projection matrices
			_effect.View = camera.View;
			_effect.Projection = camera.Projection;

			// apply the effect
			_effect.CurrentTechnique.Passes[0].Apply();

			// set the vertex buffer
			_game.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
			// set the index buffer
			_game.GraphicsDevice.Indices = _indexBuffer;
			// Draw the triangles
			_game.GraphicsDevice.DrawIndexedPrimitives(
				PrimitiveType.TriangleList, // Tye type to draw
				0,                          // The first vertex to use
				0,                          // The first index to use
				12                          // the number of triangles to draw
			);
		}
	}
}
