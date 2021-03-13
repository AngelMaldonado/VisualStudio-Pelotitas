using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pelotitas
{
    public partial class Form1 : Form
    {
        const int CHOQUE_HORIZONTAL = 1;
        const int CHOQUE_VERTICAL = 2;
        const int CHOQUE_INFERIOR_DER = 3;
        const int CHOQUE_SUPERIOR_DER = 4;
        const int CHOQUE_INFERIOR_IZQ = 5;
        const int CHOQUE_SUPERIOR_IZQ = 6;

        List<Pelota> listPelotas;
        Font drawFont;
        String str;
        public Form1()
        {
            InitializeComponent();
            listPelotas = new List<Pelota>();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Pelota p = new Pelota(e.X, e.Y);

            /*
            if (listPelotas.Count > 0)
                p.direccion = Direccion.ABAJO; // p2
            else
                p.direccion = Direccion.ARRIBA; // p1
            */
            listPelotas.Add(p);
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int contador = 1;
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.BlueViolet);
            Font drawFont = new Font("Arial", 12);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            foreach (Pelota p in listPelotas)
            {
                int num = contador;
                String str = Convert.ToString(num);
                g.DrawEllipse(pen, p.x, p.y, 20, 20);
                g.DrawString(str, drawFont, drawBrush, p.x + 3, p.y);
                contador++;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool choque;
            int sentido = 0;
            int choqueHVD = 0;
            foreach (Pelota p1 in listPelotas)
                p1.muevete(this);

            foreach (Pelota p1 in listPelotas)
            {
                foreach (Pelota p2 in listPelotas)
                {
                    choque = false;
                    // COLISION 1-> <-2     P1 VA HACIA LA DERECHA
                    if (p1.x + 20 >= p2.x - 3 && p1.x + 20 <= p2.x + 20)
                    {
                        // CHECAR SI P1 ESTA DENTRO DEL RANGO DE Y DE P2
                        if ((p1.y < p2.y && p1.y + 20 >= p2.y) || // 1--__2
                            (p1.y + 20 > p2.y + 20 && p1.y <= p2.y + 20)) // 1__--2
                        {
                            sentido = Direccion.DERECHA;
                            choque = true;
                        }

                        // CHECAR SI P1 TIENE DIRECCION DIAGONAL INF_DER O SUP_DER
                        if (p1.direccion == Direccion.DIAG_INF_DER)
                            choqueHVD = CHOQUE_INFERIOR_DER;
                        else if (p1.direccion == Direccion.DIAG_SUP_DER)
                            choqueHVD = CHOQUE_SUPERIOR_DER;
                        else if(choque)
                            choqueHVD = CHOQUE_HORIZONTAL;

                    // COLISION 2-> <-1     P1 VA HACIA LA IZQUIERDA
                    } else if(p1.x <= p2.x + 20 && p1.x > p2.x)
                    {
                        // CHECAR SI P1 ESTA DENTRO DEL RANGO DE Y DE P2
                        if (p1.y < p2.y && p1.y + 20 >= p2.y || // 1--__2
                            p1.y + 20 > p2.y + 20 && p1.y <= p2.y + 20) // 1__--2
                        {
                            sentido = Direccion.IZQUIERDA;
                            choque = true;
                        }

                        // CHECAR SI P1 TIENE DIRECCION DIAGONAL INF_IZQ O SUP_IZQ
                        if (p1.direccion == Direccion.DIAG_SUP_IZQ)
                            choqueHVD = CHOQUE_SUPERIOR_IZQ;
                        else if (p1.direccion == Direccion.DIAG_INF_IZQ)
                            choqueHVD = CHOQUE_INFERIOR_IZQ;
                        else if(choque)
                            choqueHVD = CHOQUE_HORIZONTAL;

                    // COLISION P1^ P2 CHOCA DESDE ABAJO A P2
                    } else if(p1.y <= p2.y + 23 && p1.y > p2.y)
                    {
                        // CHECAR SI P1 ESTA DENTRO DEL RANGO DE X DE P2
                        if ((p1.x + 20 > p2.x + 20 && p1.x <= p2.x + 20) ||
                            (p1.x < p2.x && p1.x + 20 >= p2.x))
                        {
                            sentido = Direccion.ABAJO;
                            choque = true;
                        }

                        // CHECAR SI P1 TIENE DIRECCION DIAGONAL SUP_IZQ O SUP_DER
                        if (p1.direccion == Direccion.DIAG_SUP_IZQ)
                            choqueHVD = CHOQUE_SUPERIOR_IZQ;
                        else if (p1.direccion == Direccion.DIAG_SUP_DER)
                            choqueHVD = CHOQUE_SUPERIOR_DER;
                        else if (choque)
                            choqueHVD = CHOQUE_VERTICAL;

                    // COLISIOIN P2^ P1 CHOCA DESDE ARRIBA A P2
                    } else if(p1.y + 20 >= p2.y - 3 && p1.y + 20 < p2.y + 20)
                    {
                        // CHECAR SI P1 ESTA DENTRO DEL RANGO DE X DE P2
                        if ((p1.x + 20 > p2.x + 20 && p1.x <= p2.x + 20) ||
                            (p1.x < p2.x && p1.x + 20 >= p2.x))
                        {
                            sentido = Direccion.ARRIBA;
                            choque = true;
                        }

                        // CHECAR SI P1 TIENE DIRECCION DIAGONAL INF_IZQ O INF_DER
                        if (p1.direccion == Direccion.DIAG_INF_IZQ)
                            choqueHVD = CHOQUE_INFERIOR_IZQ;
                        else if (p1.direccion == Direccion.DIAG_INF_DER)
                            choqueHVD = CHOQUE_INFERIOR_DER;
                        else if (choque)
                            choqueHVD = CHOQUE_VERTICAL;
                    }

                    if (choque)
                        colisionaPelotas(listPelotas.IndexOf(p1), listPelotas.IndexOf(p2), choqueHVD, sentido);
                }
                Invalidate();
            }
        }
        public void colisionaPelotas(int indiceP1, int indiceP2, int choque, int sentido)
        {
            int r = 0;
            int[] posiblesDirP1 = new int[5];
            int[] posiblesDirP2 = new int[5];
            Random rnd;

            switch (choque)
            {
                case CHOQUE_HORIZONTAL://CHOQUE HORIZONTAL
                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.ARRIBA;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.ABAJO;
                    if(sentido == Direccion.DERECHA)
                    {
                        posiblesDirP1[2] = Direccion.DIAG_SUP_IZQ;
                        posiblesDirP1[3] = Direccion.DIAG_INF_IZQ;
                        posiblesDirP1[4] = Direccion.IZQUIERDA;

                        posiblesDirP2[2] = Direccion.DIAG_SUP_DER;
                        posiblesDirP2[3] = Direccion.DIAG_INF_DER;
                        posiblesDirP2[4] = Direccion.DERECHA;
                    } else
                    {
                        posiblesDirP1[2] = Direccion.DIAG_SUP_DER;
                        posiblesDirP1[3] = Direccion.DIAG_INF_DER;
                        posiblesDirP1[4] = Direccion.DERECHA;

                        posiblesDirP2[2] = Direccion.DIAG_SUP_IZQ;
                        posiblesDirP2[3] = Direccion.DIAG_INF_IZQ;
                        posiblesDirP2[4] = Direccion.IZQUIERDA;
                    }
                    break;
                case CHOQUE_VERTICAL://CHOQUE VERTICAL
                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.IZQUIERDA;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.DERECHA;
                    if(sentido == Direccion.ABAJO)
                    {
                        posiblesDirP1[2] = Direccion.DIAG_INF_IZQ;
                        posiblesDirP1[3] = Direccion.DIAG_INF_DER;
                        posiblesDirP1[4] = Direccion.ABAJO;
                        
                        posiblesDirP2[2] = Direccion.DIAG_SUP_IZQ;
                        posiblesDirP2[3] = Direccion.DIAG_SUP_DER;
                        posiblesDirP2[4] = Direccion.ARRIBA;
                    } else
                    {
                        posiblesDirP1[2] = Direccion.DIAG_SUP_IZQ;
                        posiblesDirP1[3] = Direccion.DIAG_SUP_DER;
                        posiblesDirP1[4] = Direccion.ARRIBA;
                        
                        posiblesDirP2[2] = Direccion.DIAG_INF_IZQ;
                        posiblesDirP2[3] = Direccion.DIAG_INF_DER;
                        posiblesDirP2[4] = Direccion.ABAJO;
                    }
                    break;
                case CHOQUE_INFERIOR_DER://CHOQUE DIAGONAL INFERIOR DERECHA
                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.DIAG_SUP_DER;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.DIAG_INF_IZQ;

                    posiblesDirP1[2] = Direccion.ARRIBA;
                    posiblesDirP1[3] = Direccion.DIAG_SUP_IZQ;
                    posiblesDirP1[4] = Direccion.IZQUIERDA;

                    posiblesDirP2[2] = Direccion.DERECHA;
                    posiblesDirP2[3] = Direccion.DIAG_INF_DER;
                    posiblesDirP2[4] = Direccion.ABAJO;
                    
                    break;
                case CHOQUE_SUPERIOR_DER://CHOQUE DIAGONAL SUPERIOR DERECHA
                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.DIAG_SUP_IZQ;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.DIAG_INF_DER;

                    posiblesDirP2[2] = Direccion.ARRIBA;
                    posiblesDirP2[3] = Direccion.DERECHA;
                    posiblesDirP2[4] = Direccion.DIAG_SUP_DER;

                    posiblesDirP1[2] = Direccion.IZQUIERDA;
                    posiblesDirP1[3] = Direccion.DIAG_INF_IZQ;
                    posiblesDirP1[4] = Direccion.ABAJO;

                    break;
                case CHOQUE_INFERIOR_IZQ://CHOQUE DIAGONAL INFERIOR IZQUIERDA
                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.DIAG_SUP_IZQ;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.DIAG_INF_DER;

                    posiblesDirP1[2] = Direccion.ARRIBA;
                    posiblesDirP1[3] = Direccion.DIAG_SUP_DER;
                    posiblesDirP1[4] = Direccion.DERECHA;

                    posiblesDirP2[2] = Direccion.IZQUIERDA;
                    posiblesDirP2[3] = Direccion.DIAG_INF_IZQ;
                    posiblesDirP2[4] = Direccion.ABAJO;

                    break;
                case CHOQUE_SUPERIOR_IZQ://CHOQUE DIAGONAL SUPERIOR IZQUIERDO

                    posiblesDirP1[0] = posiblesDirP2[0] = Direccion.DIAG_SUP_DER;
                    posiblesDirP1[1] = posiblesDirP2[1] = Direccion.DIAG_INF_IZQ;

                    posiblesDirP1[2] = Direccion.DERECHA;
                    posiblesDirP1[3] = Direccion.DIAG_INF_DER;
                    posiblesDirP1[4] = Direccion.ABAJO;

                    posiblesDirP2[2] = Direccion.ARRIBA;
                    posiblesDirP2[3] = Direccion.DIAG_SUP_IZQ;
                    posiblesDirP2[4] = Direccion.IZQUIERDA;

                    break;
            }
            rnd = new Random();
            listPelotas.ElementAt(indiceP1).cambiaDireccion(posiblesDirP1[rnd.Next(0, 4)]);
            listPelotas.ElementAt(indiceP2).cambiaDireccion(posiblesDirP2[rnd.Next(0, 4)]);
        }
    }
}
