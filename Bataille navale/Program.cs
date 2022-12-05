using System.Security.Cryptography.X509Certificates;

void AfficherTab(int[,] tab)
{
	Console.WriteLine("   A B C D E F G H I J");
	for (int i = 0; i < tab.GetLength(0); i++)
	{
		if (i < 9)
		{
			Console.Write(i + 1);
			Console.Write(" |");
		}
		else
		{
			Console.Write("10|");
		}
		for (int j = 0; j < tab.GetLength(1); j++)
		{
			switch (tab[i, j])
			{
				case > 1:
					Console.BackgroundColor = ConsoleColor.Gray;
					Console.Write("  ");
					Console.ResetColor();
					break;
				case -1:
					Console.BackgroundColor = ConsoleColor.White;
					Console.Write("  ");
					Console.ResetColor();
					break;
				case <= -2:
					Console.BackgroundColor = ConsoleColor.Red;
					Console.Write("  ");
					Console.ResetColor();
					break;
				case 0:
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.Write("  ");
					Console.ResetColor();
					break;
			}
		}
		Console.WriteLine();
	}
}
void AfficherEnnemi(int[,] tab)
{
	Console.WriteLine("   A B C D E F G H I J");
	for (int i = 0; i < tab.GetLength(0); i++)
	{
		if (i < 9)
		{
			Console.Write(i + 1);
			Console.Write(" |");
		}
		else
		{
			Console.Write("10|");
		}
		for (int j = 0; j < tab.GetLength(1); j++)
		{
			switch (tab[i, j])
			{
				case -1:
					Console.BackgroundColor = ConsoleColor.White;
					Console.Write("  ");
					Console.ResetColor();
					break;
				case <= -2:
					Console.BackgroundColor = ConsoleColor.Red;
					Console.Write("  ");
					Console.ResetColor();
					break;
				default:
					Console.BackgroundColor = ConsoleColor.Blue;
					Console.Write("  ");
					Console.ResetColor();
					break;
			}
		}
		Console.WriteLine();
	}
}
void Attaque(ref int[,] tab)
{
	int l = 15;
	int c = 15;
	while (l > 10 || l < 0 || c < 0 || c > 10)
	{
		Console.Write("Attaque ex:A2: ");
		string entrée = Console.ReadLine().ToUpper();
		if (int.TryParse((entrée.Substring(1)), out c) && (entrée.Length == 2 || entrée.Length == 3))
		{
			l = "ABCDEFGHIJ".IndexOf(entrée[0]);
			c = int.Parse(entrée.Substring(1)) - 1;
		}
	}
	switch (tab[c, l])
	{
		case 0:
			tab[c, l] = -1;
			Console.Clear();
			Console.WriteLine("Manqué ! Dommage...");
			break;
		case > 1:
			tab[c, l] = -tab[c, l];
			Console.Clear();
			Console.WriteLine("Touché ! Chateux...");
			break;
	}
}
List<(int, int)> listJoueur1 = new();
List<(int, int)> listJoueur2 = new();
void PlacementBateau(ref int[,] tab, int nombreCase,ref List<(int,int)> listTotal,int numéroBateau)
{
	int sensDeRotation = 0;
	List<(int, int)> bateau = new();
	for (int i = 0; i < nombreCase; i++)
	{
		bateau.Add((0, i));
	}
	AfficherTab(tab);
	ConsoleKeyInfo touche = Console.ReadKey();
	Console.Clear();
	while (touche.Key != ConsoleKey.Enter)
	{
		Console.WriteLine($"Placez le bateau ({nombreCase} cases): ");
		for (int i = 0; i < tab.GetLength(0); i++)
		{
			for (int j = 0; j < tab.GetLength(1); j++)
			{
				if (bateau.Contains((i, j))||listTotal.Contains((i,j)))
				{
					tab[i, j] = numéroBateau;
				}
				else
				{
					tab[i, j] = 0;
				}
			}
		}

		AfficherTab(tab);
		touche = Console.ReadKey();
		Console.Clear();
		switch (touche.Key)
		{
			case ConsoleKey.LeftArrow:
				for (int i = 0; i < bateau.Count; i++)
				{
					(int x, int y) = bateau[i];
					bateau[i] = (x, y - 1);
				}
				break;
			case ConsoleKey.RightArrow:
				for (int i = 0; i < bateau.Count; i++)
				{
					(int x, int y) = bateau[i];
					bateau[i] = (x, y + 1);
				}
				break;
			case ConsoleKey.UpArrow:
				for (int i = 0; i < bateau.Count; i++)
				{
					(int x, int y) = bateau[i];
					bateau[i] = (x - 1, y);
				}
				break;
			case ConsoleKey.DownArrow:
				for (int i = 0; i < bateau.Count; i++)
				{
					(int x, int y) = bateau[i];
					bateau[i] = (x + 1, y);
				}
				break;
			case ConsoleKey.R:
				bateau.Clear();
				sensDeRotation++;
				if (sensDeRotation % 2 == 0)
				{
					for (int i = 0; i < nombreCase; i++)
					{
						bateau.Add((0, i));
					}
				}
				else
				{
					for (int i = 0; i < nombreCase; i++)
					{
						bateau.Add((i, 0));
					}
				}
				break;
			case ConsoleKey.Enter:
				listTotal.AddRange(bateau);
				bateau.Clear();
				for (int i = 0; i < tab.GetLength(0); i++)
				{
					for (int j = 0; j < tab.GetLength(1); j++)
					{
						if (listTotal.Contains((i, j)))
						{
							tab[i, j] = numéroBateau;
						}
					}
				}
				break;
		}
	}
}
int[,] joueur1 = new int[10, 10]
{
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
};
int[,] joueur2 = new int[10, 10]
{
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
	{0,0,0,0,0,0,0,0,0,0},
};
Console.WriteLine("Place tes bateaux joueur 1");
Console.WriteLine("appuie sur R pour changer le sens de rotation(par défaut: horizontal)");
PlacementBateau(ref joueur1, 5, ref listJoueur1,2);
PlacementBateau(ref joueur1, 4, ref listJoueur1,3);
PlacementBateau(ref joueur1, 3, ref listJoueur1,4);
PlacementBateau(ref joueur1, 3, ref listJoueur1,5);
PlacementBateau(ref joueur1, 2, ref listJoueur1,6);
Console.WriteLine("Place tes bateaux joueur 2");
Console.WriteLine("appuie sur R pour changer le sens de rotation(par défaut: horizontal)");
PlacementBateau(ref joueur2, 5, ref listJoueur2,2);
PlacementBateau(ref joueur2, 4, ref listJoueur2,3);
PlacementBateau(ref joueur2, 3, ref listJoueur2,4);
PlacementBateau(ref joueur2, 3, ref listJoueur2,5);
PlacementBateau(ref joueur2, 2, ref listJoueur2,6);
while (true)
{
	Console.WriteLine("Player 1");
	AfficherEnnemi(joueur2);
	Console.WriteLine();
	AfficherTab(joueur1);
	Attaque(ref joueur2);
	Console.WriteLine("Switch to player 2");
	Console.ReadKey();
	Console.Clear();
	Console.WriteLine("Player 2");
	AfficherEnnemi(joueur1);
	Console.WriteLine();
	AfficherTab(joueur2);
	Attaque(ref joueur1);
	Console.WriteLine("Switch to player 1");
	Console.ReadKey();
	Console.Clear();
}
