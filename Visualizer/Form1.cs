using System;
using System.Drawing;
using System.Windows.Forms;
using CaveGenerator.Maps;

namespace CaveGenerator.Visualizer
{
	public partial class Form1 : Form
	{
		private const int MAP_SIZE = 64;
		private const int TILE_SIZE = 5;
		private const int BITMAP_SIZE = MAP_SIZE * TILE_SIZE;
		private const int CONTROL_SPACE = 32;

		PictureBox pictureBox1 = new PictureBox();
		TrackBar wallDensity = new TrackBar();
		TrackBar rockDensity = new TrackBar();

		Random random = new Random();
		int seed;

		public Form1()
		{
			InitializeComponent();

			Size = new Size(BITMAP_SIZE + CONTROL_SPACE + 16, BITMAP_SIZE + 39);
			
			pictureBox1.Size = new Size(BITMAP_SIZE, BITMAP_SIZE);
			pictureBox1.Location = new Point(CONTROL_SPACE, 0);
			Controls.Add(pictureBox1);

			wallDensity.Orientation = Orientation.Vertical;
			wallDensity.Size = new Size(CONTROL_SPACE, 100);
			wallDensity.Location = new Point(0, 10);
			wallDensity.Minimum = 0;
			wallDensity.Maximum = 10;
			wallDensity.Value = 5;
			Controls.Add(wallDensity);

			rockDensity.Orientation = Orientation.Vertical;
			rockDensity.Size = new Size(CONTROL_SPACE, 100);
			rockDensity.Location = new Point(0, 110);
			rockDensity.Minimum = 0;
			rockDensity.Maximum = 10;
			rockDensity.Value = 5;
			Controls.Add(rockDensity);

			seed = random.Next();
			CreateBitmap();
			pictureBox1.Click += (sender, e) =>
			{
				seed = random.Next();
				CreateBitmap();
			};
			wallDensity.ValueChanged += (sender, e) => CreateBitmap();
			rockDensity.ValueChanged += (sender, e) => CreateBitmap();
		}

		public void CreateBitmap()
		{
			IHeightMap map = new Cave(seed, MAP_SIZE, MAP_SIZE, wallDensity.Value / 10.0, rockDensity.Value / 10.0);

			Bitmap bmp = new Bitmap(BITMAP_SIZE, BITMAP_SIZE);
			Graphics gfx = Graphics.FromImage(bmp);
			for (int i = 0; i < MAP_SIZE; i++)
			{
				for (int j = 0; j < MAP_SIZE; j++)
				{
					Brush brush = Brushes.White;
					if (map[i, j] >= 3) brush = Brushes.Black;
					else if (map[i, j] == 2) brush = Brushes.Gray;
					else if (map[i, j] == 1) brush = Brushes.LightGray;

					gfx.FillRectangle(brush, i * TILE_SIZE, j * TILE_SIZE, TILE_SIZE, TILE_SIZE);
				}
			}

			pictureBox1.Image = bmp;
		}
	}
}
