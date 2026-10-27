using System.Runtime;
using System.Xml.Serialization;

int x_dim = 7;
int y_dim = 6;

char[,] coord = new char[y_dim, x_dim];

static void ScanField(char[,] tablero)
{  
    int filas = tablero.GetLength(0);
    int columnas = tablero.GetLength(1);
    for (int i = 0; i < filas; i++)
    {
        for (int a = 0; a < columnas; a++)
        {
            char valor = tablero[i, a];
            string ficha = " ";

            if (valor.Equals('X'))
            {
                ficha = "X";
            }
            else if (valor.Equals('O'))
            {
                ficha = "O";
            }
            Console.Write($"| {ficha} ");
        }
        Console.WriteLine("|");
    }

    Console.WriteLine("  1  2  3  4  5  6  7");
    //Console.WriteLine(x); 
    //Console.WriteLine(y);

}

static bool UserPiece(char[,] tablero, bool turno)
{
    Console.WriteLine("Elige la posición (de 1 a 7): ");
    int.TryParse(Console.ReadLine(), out int choice);
    while (choice-1 < 0 || choice-1 > 6)
    {
        Console.Clear();
        Console.WriteLine("Elige un número válido.");
        Console.WriteLine("Elige la posición (de 1 a 7): ");
        int.TryParse(Console.ReadLine(), out choice);
    }

    int columna = choice - 1;
    char jugadorActivo;

    if (turno == true)
    {
        jugadorActivo = 'X';
    }
    else
    {
        jugadorActivo = 'O';
    }
    if (tablero[0, columna] == 'X' || tablero[0, columna] == 'O')
    {
        Console.WriteLine("Esa línea ya está llena. Elige otra");
        return true;
    }
    for (int fila = tablero.GetLength(0) - 1; fila >= 0; fila--)
    {
        if (tablero[fila, columna] == 'X' || tablero[fila, columna] == 'O')
        {

        }
        else
        {
            tablero[fila, columna] = jugadorActivo;
            return false;
        }
    }
    return true;
}

static bool TurnLogic(int turn)
{
    if (turn % 2 == 0)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//TODO: Win condition
static bool WinCheck(char[,] tablero)
{
    int filas = tablero.GetLength(0);
    int columnas = tablero.GetLength(1);

    // Horizontal
    for (int f = 0; f < filas; f++)
    {
        for (int c = 0; c <= columnas - 4; c++)
        {
            char ficha = tablero[f, c];
            if (ficha == '\0') continue;

            if ((ficha == 'X' || ficha == 'O') &&
                ficha == tablero[f, c + 1] &&
                ficha == tablero[f, c + 2] &&
                ficha == tablero[f, c + 3])
            {
                return true;
            }
        }
    }

    // Vertical
    for (int f = 0; f <= filas - 4; f++)
    {
        for (int c = 0; c < columnas; c++)
        {
            char ficha = tablero[f, c];
            if (ficha == '\0') continue;

            if ((ficha == 'X' || ficha == 'O') &&
                ficha == tablero[f + 1, c] &&
                ficha == tablero[f + 2, c] &&
                ficha == tablero[f + 3, c])
            {
                return true;
            }
        }
    }

    // Diagonal descendente / derecha
    for (int f = 0; f <= filas - 4; f++)
    {
        for (int c = 0; c <= columnas - 4; c++)
        {
            char ficha = tablero[f, c];
            if (ficha == '\0') continue;

            if ((ficha == 'X' || ficha == 'O') &&
                ficha == tablero[f + 1, c + 1] &&
                ficha == tablero[f + 2, c + 2] &&
                ficha == tablero[f + 3, c + 3])
            {
                return true;
            }
        }
    }

    // Diagonal ascendente / derecha
    for (int f = 3; f < filas; f++)
    {
        for (int c = 0; c <= columnas - 4; c++)
        {
            char ficha = tablero[f, c];
            if (ficha == '\0') continue;

            if ((ficha == 'X' || ficha == 'O') &&
                ficha == tablero[f - 1, c + 1] &&
                ficha == tablero[f - 2, c + 2] &&
                ficha == tablero[f - 3, c + 3])
            {
                return true;
            }
        }
    }

    return false;
}



//Console.WriteLine(coord.Length);
//Console.WriteLine(coord.GetLength(0));
int round = 0;
bool playerTurn;
bool running = true;

while (running)
{
    playerTurn = TurnLogic(round);

    ScanField(coord);
    string jugador;
    if (playerTurn == true)
    {
        jugador = "Jugador X";
    }
    else
    {
        jugador = "Jugador O";
    }
    bool restart = UserPiece(coord, playerTurn);
    if (restart)
    {
        continue;
    }
    if (WinCheck(coord) == true)
    {
        ScanField(coord);
        Console.WriteLine($"Enhorabuena! El jugador {jugador} gana!");
        running = false;
    }
    round++;
}
