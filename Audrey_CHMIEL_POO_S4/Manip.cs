using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Media;
using System.ComponentModel;
using System.IO;

namespace Audrey_CHMIEL_POO_S4
{
    public class Manip
    {
        //champ d'instance
        byte[,][] matPixel;
        int largeur;
        int hauteur;


        //constructeur

        public Manip(byte[,][] matPixel, int largeur, int hauteur)
        {
            this.matPixel = matPixel;
            this.largeur = largeur;
            this.hauteur = hauteur;
        }


        public byte[,][] Convert_To_Nuances_Gris()
            //les 3 couleurs en qtité égale codent du gris
        {
            
            int moyenne = 0;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    moyenne = (matPixel[i, j][0] + matPixel[i, j][1] + matPixel[i, j][2])/3;
                    matPixel[i, j][0] = Convert.ToByte(moyenne);
                    matPixel[i, j][1] = Convert.ToByte(moyenne);
                    matPixel[i, j][2] = Convert.ToByte(moyenne);

                    moyenne = 0;

                }
            }
            return matPixel;
            /*affichage
            for (int i = 0; i < matPixel.GetLength(0); i++)
            {
                for (int j = 0; j < matPixel.GetLength(1); j++)
                {
                    Console.Write(matPixel[i, j][0]);
                    Console.Write(matPixel[i, j][1]);
                    Console.Write(matPixel[i, j][2]);
                    Console.ReadKey();
                }
                Console.WriteLine();
            */
            
                 
        }
    }
}


