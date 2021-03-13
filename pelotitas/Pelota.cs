using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class Paredes{
    public const int PARED_SUP = 1;
    public const int PARED_INF = 2;
    public const int PARED_IZQ = 3;
    public const int PARED_DER = 4;
}

static class Direccion {
    public const int ARRIBA = 1;
    public const int ABAJO = 2;
    public const int IZQUIERDA = 3;
    public const int DERECHA = 4;
    public const int DIAG_SUP_IZQ = 5;
    public const int DIAG_SUP_DER = 6;
    public const int DIAG_INF_IZQ = 7;
    public const int DIAG_INF_DER = 8;
}

namespace pelotitas
{
    class Pelota
    {
        public const int velocidad = 10;
        public int x;
        public int y;
        public int direccion;

        public Pelota()
        {

        }

        public Pelota(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.direccion = randomDireccion();
            
        }

        public int randomDireccion()
        {
            Random rnd = new Random();
            return rnd.Next(1, 8);
        }

        public void cambiaDireccion(int dir)
        {
            direccion = dir;
        }

        public void muevete(Form1 form)
        {
            switch (direccion)
            {
                case Direccion.DERECHA://DERECHA
                    if (x + 40 < form.Width)
                        x += velocidad;
                    else
                        colisionaPared(Paredes.PARED_DER);
                    break;
                case Direccion.IZQUIERDA://IZQUIERDA
                    if (x > 0)
                        x -= velocidad;
                    else
                        colisionaPared(Paredes.PARED_IZQ);
                    break;
                case Direccion.ARRIBA://ARRIBA
                    if (y - 5 > 0 )
                        y -= velocidad;
                    else
                        colisionaPared(Paredes.PARED_SUP);
                    break;
                case Direccion.ABAJO://ABAJO
                    if (y + 60 < form.Height)
                        y += velocidad;
                    else
                        colisionaPared(Paredes.PARED_INF);
                    break;
                case Direccion.DIAG_SUP_IZQ://DIAGONAL SUPERIOR IZQUIERDA
                    if (x - 5 > 0 && y - 5 > 0)
                    {
                        x -= velocidad;
                        y -= velocidad;
                    }
                    else
                    {
                        if (y - 10 <= 0)
                            colisionaPared(Paredes.PARED_SUP);//ARRIBA
                        else
                            colisionaPared(Paredes.PARED_IZQ);//IZQUIERDA
                    }
                    break;
                case Direccion.DIAG_INF_IZQ://DIAGONAL INFERIOR IZQUIERDA
                    if (x - 5 > 0 && y + 60 < form.Height)
                    {
                        x -= velocidad;
                        y += velocidad;
                    }
                    else
                    {
                        if (y + 60 >= form.Height)
                            colisionaPared(Paredes.PARED_INF);
                        else
                            colisionaPared(Paredes.PARED_IZQ);
                    }
                    break;
                case Direccion.DIAG_SUP_DER://DIAGONAL SUPERIOR DERECHA
                    if (x + 40 < form.Width && y - 5 > 0)
                    {
                        x += velocidad;
                        y -= velocidad;
                    }
                    else
                    {
                        if (y - 5 <= 0)
                            colisionaPared(Paredes.PARED_SUP);
                        else
                            colisionaPared(Paredes.PARED_DER);
                    }
                    break;
                case Direccion.DIAG_INF_DER://DIAGONAL INFERIOR DERECHA
                    if (x + 40 < form.Width && y + 60 < form.Height)
                    {
                        x += velocidad;
                        y += velocidad;
                    }
                    else
                    {
                        if (y + 60 >= form.Height)
                            colisionaPared(Paredes.PARED_INF);
                        else
                            colisionaPared(Paredes.PARED_DER);
                    }
                    break;
            }
        }

        
        public void colisionaPared(int pared)
        {
            int[] posiblesDir = new int[3];
            Random rnd = new Random();

            switch (pared)
            {
                case Paredes.PARED_SUP://PARED SUPERIOR
                    posiblesDir[0] = Direccion.ABAJO;//ABAJO
                    posiblesDir[1] = Direccion.DIAG_INF_IZQ;
                    posiblesDir[2] = Direccion.DIAG_INF_DER;
                    break;
                case Paredes.PARED_INF://PARED INFERIOR
                    posiblesDir[0] = Direccion.ARRIBA;
                    posiblesDir[1] = Direccion.DIAG_SUP_IZQ;
                    posiblesDir[2] = Direccion.DIAG_SUP_DER;
                    break;
                case Paredes.PARED_IZQ://PARED IZQUIERDA
                    posiblesDir[0] = Direccion.DERECHA;//DERECHA
                    posiblesDir[1] = Direccion.DIAG_SUP_DER;
                    posiblesDir[2] = Direccion.DIAG_INF_DER;
                    break;
                case Paredes.PARED_DER://PARED DERECHA
                    posiblesDir[0] = Direccion.IZQUIERDA;
                    posiblesDir[1] = Direccion.DIAG_SUP_IZQ;
                    posiblesDir[2] = Direccion.DIAG_INF_IZQ;
                    break;
            }
            direccion = posiblesDir[rnd.Next(posiblesDir.Length)];
        }
    }
}
