using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursov2
{
    public partial class Form1 : Form
    {
        GravityPoint point1; // добавил поле под первую точку
        GravityPoint point2;
        GravityPoint point3;
        GravityPoint point4;
        GravityPoint point5;
        GravityPoint point6;
        GravityPoint point7;
        List<Particle> particles = new List<Particle>();
        List<TopEmitter> emitters = new List<TopEmitter>();
        TopEmitter emitter;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            this.emitter = new TopEmitter // создаю эмиттер и привязываю его к полю emitter
            {
                Width = picDisplay.Width,
                GravitationY = 0.25f
                /*Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,*/
            };

            emitters.Add(this.emitter);
            point1 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 100,
                Y = picDisplay.Height / 2+30,
            };
            point2 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2+30,
            };
            point3 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 200,
                Y = picDisplay.Height / 2+60,
            };
            point4 = new GravityPoint
            {
                X = picDisplay.Width / 2 - 300,
                Y = picDisplay.Height / 2+90,
            };
            point5 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 200,
                Y = picDisplay.Height / 2+60,
            };
            point6 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 300,
                Y = picDisplay.Height / 2+90,
            };
            point7 = new GravityPoint
            {
                X = picDisplay.Width / 2 ,
                Y = picDisplay.Height / 2,
            };

            // привязываем поля к эмиттеру
            emitter.impactPoints.Add(point1);
            emitter.impactPoints.Add(point2);
            emitter.impactPoints.Add(point3);
            emitter.impactPoints.Add(point4);
            emitter.impactPoints.Add(point5);
            emitter.impactPoints.Add(point6);
            emitter.impactPoints.Add(point7);



        }

        private void picDisplay_Click(object sender, EventArgs e)
        {
            
        }
        // добавил функцию обновления состояния системы
        
        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            emitter.UpdateState(); // каждый тик обновляем систему

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // рендерим систему
            }
            picDisplay.Invalidate();
        }
        
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }
            
        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = trackBar1.Value;
        }

        private void tbGravitation_Scroll(object sender, EventArgs e)
        {
            foreach (var p in emitter.impactPoints)
            {
                if (p is GravityPoint) // так как impactPoints не обязательно содержит поле Power, надо проверить на тип 
                {
                    // если гравитон то меняем силу
                    (p as GravityPoint).Power = tbGravitation.Value;
                }
            }
        }
    }
}
