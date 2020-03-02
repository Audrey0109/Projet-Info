using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Media;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Audrey_CHMIEL_POO_S4
{
    class Program
    {
        static void Main(string[] args)
        {
            MyImage a = new MyImage("coco.bmp");
            //MyImage b = new MyImage("Sortie.bmp"); //perroquet sauvegardé
            //Console.WriteLine(a.toString());
            //a.Sauvegarde("Sortie.bmp", a.MatPixel);
            
            a.MatriceTableauPixel();
            
            Manip b = new Manip(a.MatPixel, a.Largeur, a.Hauteur);
            byte[,][] newMatGrise = b.Convert_To_Nuances_Gris();
            a.Sauvegarde("Sortie.bmp", newMatGrise);
            Process.Start("Sortie.bmp"); //fonction qui permet d'aller chercher un fichier et de l'ouvrir directement

            Console.ReadKey();

            //Mes objectifs pour la prochaine séance:
            //coder en niveaux de gris 
            //miroir
        }
    }
}
