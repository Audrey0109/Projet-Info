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
    class MyImage
    {

        //champ d'instance

        string type = "";
        int tailleOffset;
        int tailleFichier;
        int largeur;
        int hauteur;
        int nbBitsCouleur;
        byte[,][] matPixel;
        byte[,] matRGB;
        int TailleMyFile;
        byte[] Header = new byte[54];

        //constructeur

        public MyImage(string file)
        {

            //lecture From_Image_To_File
            byte[] myfile = File.ReadAllBytes(file);

            TailleMyFile = myfile.Length;



            /*if(myfile[0]==66 && myfile[1] == 77)
            {
                Console.WriteLine("BITMAP");
            }
            */

            //faire lecture pour tous les fichiers et pas que bitmap


            //Convertir Endian to Bytes
            for (int i = 0; i < 2; i++) //ASCII
            {
                type += Convert.ToString((char)myfile[i]);
                Header[i] = myfile[i];
            }
            for (int i = 2; i < 6; i++)
            {
                tailleFichier += myfile[i] * Convert.ToInt32(Math.Pow(256, i - 2));
                Header[i] = myfile[i];
            }
            for (int i = 6; i < 10; i++)
            {
                Header[i] = myfile[i];
            }
            for (int i = 10; i < 14; i++)
            {
                tailleOffset += myfile[i] * Convert.ToInt32(Math.Pow(256, (i - 10)));
                Header[i] = myfile[i];
            }
            for (int i = 14; i < 18; i++)
            {
                Header[i] = myfile[i];
            }
            for (int i = 18; i < 22; i++)
            {
                largeur += myfile[i] * Convert.ToInt32(Math.Pow(256, (i - 18)));
                Header[i] = myfile[i];
            }
            for (int i = 22; i < 26; i++)
            {
                hauteur += myfile[i] * Convert.ToInt32(Math.Pow(256, (i - 22)));
                Header[i] = myfile[i];
            }
            for (int i = 26; i < 28; i++)
            {
                Header[i] = myfile[i];
            }
            for (int i = 28; i < 30; i++)
            {
                nbBitsCouleur += myfile[i] * Convert.ToInt32(Math.Pow(256, (i - 28)));
                Header[i] = myfile[i];
            }
            for (int i = 30; i < 54; i++)
            {
                Header[i] = myfile[i];
            }

            //création matrice RGB

            matRGB = new byte[hauteur, largeur * 3];
            int k = 54;
            for (int i = 0; i < matRGB.GetLength(0); i++)
            {
                for (int j = 0; j < matRGB.GetLength(1); j++)
                {
                    matRGB[i, j] = myfile[k];
                    k++;
                }
            }

        }

        //propriétés
        public string Type
        {
            get { return this.type; }
        }

        public int TailleOffset
        {
            get { return this.tailleOffset; }
        }

        public int TailleFichier
        {
            get { return this.tailleFichier; }
        }

        public int Largeur
        {
            get { return this.largeur; }
        }

        public int Hauteur
        {
            get { return this.hauteur; }
        }

        public int NbBitsCouleur
        {
            get { return this.nbBitsCouleur; }
        }

        public byte[,] MatRGB
        {
            get { return this.matRGB; }
        }

        public byte[,][] MatPixel
        {
            get { return this.matPixel; }
        }


        /// <summary>
        /// Affichage sur la console
        /// </summary>
        /// <returns>chaine de caractère décrivant l'image</returns>
        public string toString()
        {
            string chaine = "TYPE : " + type + "\n";
            chaine += "TAILLE FICHIER  " + tailleFichier + "\n";
            chaine += "TAILLE OFFSET  " + tailleOffset + "\n";
            chaine += "HAUTEUR: " + hauteur + "\n";
            chaine += "LARGEUR : " + largeur + "\n";
            chaine += "NOMBRE DE BITS PAR COULEUR : " + nbBitsCouleur + "\n";
            chaine += "MATRICE DE PIXEL =";

            for (int i = 0; i < matRGB.GetLength(0); i++)
            {
                for (int j = 0; j < matRGB.GetLength(1); j++)
                {
                    Console.Write(matRGB[i, j] + " ");
                }
                Console.WriteLine();
            }
            return chaine;
        }



        /// <summary>
        /// Methode qui transcrit l'image de bytes en little endian
        /// </summary>
        /// <param name="file"></param>
        public void Sauvegarde(string file, byte [,][] matricePixel)
        {
            string Sortie = "" + file;
            byte[] Tab = new byte[TailleMyFile];
            if (file == "Sortie.bmp")
            {
                for (int i = 0; i < Header.Length; i++)
                {
                    Tab[i] = Header[i];
                }
                Tab[18] = Convert.ToByte(largeur % 256);
                Tab[19] = Convert.ToByte((largeur - Tab[19]) / 256);
                Tab[22] = Convert.ToByte(hauteur % 256);
                Tab[23] = Convert.ToByte((hauteur - Tab[22]) / 256);

            }
            int k = 54;
            for (int i = 0; i < hauteur; i++)
            {
                for (int j = 0; j < largeur; j++)
                {
                    for(int l = 0; l < 3; l++)
                    {
                        Tab[k] = Convert.ToByte(matricePixel[i, j][l]); 
                        k++;
                        
                    }

                }
            }
            File.WriteAllBytes(Sortie, Tab);
                 
        }

        public void MatriceTableauPixel()
        {

            int k = 0;
            matPixel = new byte[hauteur, largeur][];

            for (int i = 0; i < matPixel.GetLength(0); i++)
            {
                for (int j = 0; j < matPixel.GetLength(1) && k < matRGB.GetLength(1); j++)
                {
                    matPixel[i, j] = new byte[3];
                    matPixel[i, j][0] = Convert.ToByte(matRGB[i, k]);
                    matPixel[i, j][1] = Convert.ToByte(matRGB[i, k + 1]);
                    matPixel[i, j][2] = Convert.ToByte(matRGB[i, k + 2]);
                    k = k + 3;

                }
                k = 0;

            }
            //affichage
            /*
            for (int i = 0; i < matPixel.GetLength(0); i++)
            {
                for (int j = 0; j < matPixel.GetLength(1); j++)
                {
                    Console.Write(matPixel[i, j][0]);
                    Console.Write(matPixel[i, j][1]);
                    Console.Write(matPixel[i, j][2]);
                }
                Console.WriteLine();
            }
            */

        }

    }
}

