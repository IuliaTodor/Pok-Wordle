using System;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata;

namespace Wordle;
public class Wordle
{
    static void Main()
    {
        //string path = Path.GetFullPath(@".\");

        string fullPathFiles = Directory.GetCurrentDirectory();
        Directory.CreateDirectory(fullPathFiles);

        Wordle w = new Wordle();

        w.Intro(fullPathFiles);

    }
    //Esta función imprime los títulos de las pantallas. Es decir, aquellos en rojo y blanco
    void ImprimirTitulo(string fullPathFiles, string ruta)
    {
        Console.Clear();

        string tituloFile = fullPathFiles + ruta;
        string[] titulo = File.ReadAllLines(tituloFile);

        //Pinta la mitad de rojo y la otra de blanco
        for (int i = 0; i < titulo.Length; i++)
        {
            if (i < titulo.Length / 2)
            {
                Color(titulo[i], ConsoleColor.Red);
            }

            else
            {
                Color(titulo[i], ConsoleColor.White);
            }

        }
    }
    //Cambia el color de la letra e imprime el texto
    void Color(string letra, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(letra);
        Console.ResetColor();
    }

    //Imprime el texto de un fichero
    void ImprimirTexto(string fullPathFiles, string ruta)
    {
        string textoFile = fullPathFiles + ruta;
        string[] texto = File.ReadAllLines(textoFile);

        for (int i = 0; i < texto.Length; i++)
        {
            Console.WriteLine(texto[i] + "\n");
        }
    }

    //Lee el texto de un fichero y le hace un Split \r\n
    public static string[] SplitRN(string path)
    {
        string[] text = File.ReadAllText(path).Split("\r\n");

        return text;
    }
    void Intro(string fullPathFiles)
    {
        string? opcio;
        //El fichero donde se encuentra el texto del juego
        string textoJuegoFile = fullPathFiles + @"\Texto\TextoJuego.txt";
        string[] textoJuego = SplitRN(textoJuegoFile);

        int selectorModo = 0;
        string nombreJugador = "";

        do
        {
            ImprimirTitulo(fullPathFiles, @"\Titulos\Titulo.txt"); //Imprime Pokewordle
            ImprimirTexto(fullPathFiles, @"\Texto\OpcionesMenuTexto.txt"); //Imprime las opciones del menú

            opcio = Console.ReadLine();

            switch (opcio)
            {
                case "1":
                    nombreJugador = Game(fullPathFiles, selectorModo);
                    break;
                case "2":
                    selectorModo = Modo(fullPathFiles);
                    break;
                case "3":
                    Reglas(fullPathFiles);
                    break;
                case "4":
                    Historial(fullPathFiles, nombreJugador);
                    break;
                case "5":
                    Salir(fullPathFiles);
                    break;
                default:
                    Console.WriteLine(textoJuego[4]); //Imprime: Opción Incorrecta. Pulsa Enter para continuar.
                    break;
            }

            Console.ReadLine();

        } while (opcio != "5");

    }
    //Explica las dificultades
    void ExplicacionDificultad(string fullPathFiles)
    {
        Console.Clear();

        ImprimirTexto(fullPathFiles, @"\Dificultad\DificultadExplicarTexto.txt");

        Console.ReadLine();

        Console.Clear();
    }
    //Selecciona la dificultad
    int Dificultad(string fullPathFiles)
    {
        string seleccionarDificultad = "a";

        //En este fichero se encuentran enumeradas las dificultades del juego
        string dificultadFile = fullPathFiles + @"\Dificultad\EnumerarDificultades.txt";
        string[] dificultades = SplitRN(dificultadFile);
        string textoJuegoFile = fullPathFiles + @"\Texto\TextoJuego.txt";
        string[] textoJuego = SplitRN(textoJuegoFile);

        int dificultad = 0;

        do
        {
            Console.Clear();

            //El selector de dificultades
            ImprimirTexto(fullPathFiles, @"\Dificultad\DificultadSeleccionarTexto.txt");

            seleccionarDificultad = Console.ReadLine();

            switch (seleccionarDificultad)
            {
                case "1":
                    dificultad = 1;
                    //textoJuego[2] es "Has seleccionado la dificultad:" y dificultades[]
                    //es la dificultad dependiendo de la que haya seleccionado el usuario
                    Console.WriteLine(textoJuego[2] + " " + dificultades[0]);
                    Console.ReadLine();
                    break;
                case "2":
                    dificultad = 2;
                    Console.Write(textoJuego[2] + " " + dificultades[1]);
                    Console.ReadLine();
                    break;
                case "3":
                    dificultad = 3;
                    Console.Write(textoJuego[2] + " " + dificultades[2]);
                    Console.ReadLine();
                    break;
                case "4":
                    dificultad = 4;
                    Console.Write(textoJuego[2] + " " + dificultades[3]);
                    Console.ReadLine();
                    break;
                case "5":
                    ExplicacionDificultad(fullPathFiles);
                    break;
                default:
                    Console.Write(textoJuego[4]); //Imprime: Opción Incorrecta. Pulsa Enter para continuar.
                    Console.ReadLine();
                    break;
            }

        } while (dificultad == 0);

        Console.Clear();

        return dificultad;
    }

    //Hace un split de : de una parte de una lista
    public static string[] SplitTwoDots(List<string> list, int indice)
    {
        string[] modos = list[indice].Split(':');

        return modos;
    }

    //Asigna el string modo (dentro de Game) según el modo que haya seleccionado el usuario
    public static string GetMode(int selectorModo, string modo, string[] defaultGen, string[] P14, string[] P59)
    {
        switch (selectorModo)
        {
            case 0:
                modo = defaultGen[1];
                break;
            case 1:
                modo = P14[1];
                break;
            case 2:
                modo = P59[1];
                break;
        }
        return modo;
    }

    //Devuelve un fichero con palabras
    public static string[] ChargeFile(string fullPathFiles, string[] palabras, string dificultades, string stringModo)
    {
        palabras = File.ReadAllLines(fullPathFiles + @"\Palabras\" + stringModo + dificultades + ".txt");
        return palabras;
    }

    //Devuelve un fichero según el modo que haya seleccionado el usuario
    public static string[] CheckMode(int selectorModo, string[] palabras, string fullPathFiles, string dificultades, string[] DefaultGen, string[] P14Gen, string[] P59Gen)
    {
        switch (selectorModo)
        {
            //Si selectorModo es 0 carga el archivo por defecto
            //(en este caso los Pokémon de primera a cuarta generación, pero se puede modificar en el fichero.
            //Si es 1 carga de 1-4 generación. Si es 2 carga de 5-9 generación.
            case 0:
                palabras = ChargeFile(fullPathFiles, palabras, dificultades, DefaultGen[1]);
                break;
            case 1:
                palabras = ChargeFile(fullPathFiles, palabras, dificultades, P14Gen[1]);
                break;
            case 2:
                palabras = ChargeFile(fullPathFiles, palabras, dificultades, P59Gen[1]);
                break;
        }
        return palabras;
    }

    string Game(string fullPathFiles, int selectorModo)
    {
        Console.Clear();

        string textoJuegoFile = fullPathFiles + @"\Texto\TextoJuego.txt";//texto del juego
        string enumerarModosFile = fullPathFiles + @"\Modo\EnumerarModos.txt";//enumerar los modos de juego
        string enumerarDificultadFile = fullPathFiles + @"\Dificultad\EnumerarDificultades.txt";//enumerar dificultades
        string numeroLetrasFile = fullPathFiles + @"\Dificultad\NumeroLetras.txt";//numero de letras según dificultad
        string intentosTotalesFile = fullPathFiles + @"\Dificultad\IntentosTotales.txt";//numero intentos según dificultad

        string[] textoJuego = SplitRN(textoJuegoFile);
        string[] dificultades = SplitRN(enumerarDificultadFile);
        string[] numeroLetrasString = SplitRN(numeroLetrasFile);
        string[] intentosTotalesString = SplitRN(intentosTotalesFile);

        //Creamos una lista con el contenido de enumerarModos
        List<string> listaModos = new List<string>();
        using (StreamReader sr = File.OpenText(enumerarModosFile))
        {
            string valor;

            while ((valor = sr.ReadLine()) != null)
            {
                listaModos.Add(valor);
            }
        }

        //Sacamos los string Pokemon1-4Gen, Pokemon5-9Gen y el string por default
        string[] stringPokemon14Gen = SplitTwoDots(listaModos, 0);
        string[] stringPokemon59Gen = SplitTwoDots(listaModos, 1);
        string[] stringDefaultGen = SplitTwoDots(listaModos, 2);

        string tryAgain = "A";
        string nombreJugador = "";

        do
        {
            //reiniciamos las variables que usaremos
            int selectorDificultad = 0;
            int numeroLetras = 0;
            int intentosTotales = 0;
            int intentosRestantes = intentosTotales;

            //las palabras son el fichero que cargaremos según el modo y dificultad
            string[] palabras = { "a" };
            //estos dos strings servirán para guardarlos en el historial más tarde
            string dificultad = "a";
            string modo = "b";

            //Variable usada para la posición de las letras en las palabras
            int position = 0;

            modo = GetMode(selectorModo, modo, stringDefaultGen, stringPokemon14Gen, stringPokemon59Gen);

            selectorDificultad = Dificultad(fullPathFiles);

            //selecciona la dificultad según el modo que haya introducido el usuario. 1 carga fácil, 2 normal,
            //3 difícil y 4 extremo.
            switch (selectorDificultad)
            {
                case 1:
                    numeroLetras = Convert.ToInt32(numeroLetrasString[0]);//5
                    intentosTotales = Convert.ToInt32(intentosTotalesString[0]);//7
                    dificultad = dificultades[0];//Facil
                    palabras = CheckMode(selectorModo, palabras, fullPathFiles, dificultades[0], stringDefaultGen, stringPokemon14Gen, stringPokemon59Gen);
                    break;
                case 2:
                    numeroLetras = Convert.ToInt32(numeroLetrasString[1]);//6
                    intentosTotales = Convert.ToInt32(intentosTotalesString[1]);//6
                    dificultad = dificultades[1];//Normal
                    palabras = CheckMode(selectorModo, palabras, fullPathFiles, dificultades[1], stringDefaultGen, stringPokemon14Gen, stringPokemon59Gen);
                    break;
                case 3:
                    numeroLetras = Convert.ToInt32(numeroLetrasString[2]);//7
                    intentosTotales = Convert.ToInt32(intentosTotalesString[2]);//5
                    dificultad = dificultades[2];//Difícil
                    palabras = CheckMode(selectorModo, palabras, fullPathFiles, dificultades[2], stringDefaultGen, stringPokemon14Gen, stringPokemon59Gen);
                    break;
                case 4:
                    numeroLetras = Convert.ToInt32(numeroLetrasString[3]);//8
                    intentosTotales = Convert.ToInt32(intentosTotalesString[3]);//4
                    dificultad = dificultades[3];//Extremo
                    palabras = CheckMode(selectorModo, palabras, fullPathFiles, dificultades[3], stringDefaultGen, stringPokemon14Gen, stringPokemon59Gen);
                    break;
                default:
                    Console.WriteLine(textoJuego[7]); //Imprime: Comando inválido
                    break;
            }
            //Imprime: Introduce un Pokémon de X letras y de la Pokédex que hayas seleccionado
            Console.WriteLine(textoJuego[0] + numeroLetras + " " + textoJuego[1]);
            //Imprime: Pokédex:
            Console.WriteLine(textoJuego[16] + " " + modo);
            //Imprime: Intentos totales:
            Console.WriteLine(textoJuego[8] + ":" + " " + intentosTotales);
            string? guess = "a";

            //Generamos una palabra aleatoria entre el fichero
            Random random = new Random();
            int randomNumber = random.Next(palabras.Length);
            string randomWord = palabras[randomNumber];
            //Transformamos la palabra a mayúscula
            randomWord = randomWord.ToUpper();

            intentosRestantes = intentosTotales;
            //Convertimos los intentosTotales a String para luego guardarlos en el Historial
            string intentosTotalesToString = Convert.ToString(intentosTotales);
            do
            {
                //He dejado esta línea comentada. Es para que aparezca la palabra random antes de empezar. La estaba
                //usando cuando hacía pruebas, pero si la necesitas aquí está
                //Console.WriteLine(randomWord);

                bool isTheGuessAPokemon = false;

                //pide la palabra al usuario y comprueba si es un Pokémon dentro del fichero
                guess = IsTheWordAPokemon(isTheGuessAPokemon, palabras, textoJuego, numeroLetras);

                //Comprueba las posiciones de las letras
                CheckGuess(guess, randomWord, position);

                //Pasamos los intentos restantes a String para usarlo en el historial
                string intentosRestantesString = intentosRestantes.ToString();

                if (randomWord == guess)
                {
                    Console.WriteLine();
                    Console.WriteLine(textoJuego[6]);//Imprime: ¡Has adivinado la palabra!
                    Console.ReadLine();
                    ImprimirTitulo(fullPathFiles, @"\Titulos\Victoria.txt");
                    //Pregunta el nombre al usuario y añade sus datos a una lista
                    nombreJugador = AñadirNombre(textoJuego, intentosRestantesString, intentosTotalesToString, dificultad, modo, randomWord, guess, fullPathFiles);
                    Console.ReadLine();
                    break;
                }
                //Si la palabra no es la oculta, los intentosRestantes disminuyen 
                else
                {
                    intentosRestantes--;
                    Console.WriteLine();
                    Console.WriteLine(textoJuego[5] + ":" + intentosRestantes + "\n");
                }
                //El bucle continúa hasta que el usuario se quede sin intentos
            } while (intentosRestantes != 0);

            //Si el usuario se ha quedado sin intentos aparece esto
            if (intentosRestantes == 0)
            {
                Console.WriteLine(textoJuego[9]);//Imprime: ¡Te has quedado sin intentos!
                Console.ReadLine();
                ImprimirTitulo(fullPathFiles, @"\Titulos\Derrota.txt");
                //Imprime: Has perdido. La respuesta correcta era: randomword
                Console.WriteLine(textoJuego[10] + " " + randomWord + "\n");
                //Pedimos el nombre al jugador y añadimos los datos
                nombreJugador = AñadirNombre(textoJuego, "0", intentosTotalesToString, dificultad, modo, randomWord, guess, fullPathFiles);
            }

            do
            {
                Console.Clear();

                Console.WriteLine(textoJuego[11] + '\n');//¿Quieres volver a jugar?
                Console.WriteLine(textoJuego[12]); //S:Volver a jugar
                Console.WriteLine(textoJuego[13]);//N: Salir
                //Pregunta al usuario si quiere volver a jugar
                tryAgain = Console.ReadLine().ToUpper();

                switch (tryAgain)
                {
                    case "S":
                        Game(fullPathFiles, selectorModo);
                        break;
                    case "N":
                        break;
                    default:
                        Console.WriteLine(textoJuego[4]);//Imprime: Opción Incorrecta. Pulsa Enter para continuar.
                        Console.ReadLine();
                        break;
                }
            } while (tryAgain != "S" && tryAgain != "N");

        } while (tryAgain != "N");

        return nombreJugador;
    }
    //Guess es la palabra que introduce el usuario. Si es un Pokémon dentro del fichero entonces saldrá del bucle,
    //pero de lo contrario tendrá que introducir algo válido
    string IsTheWordAPokemon(bool isTheGuessAPokemon, string[] palabras, string[] textoJuego, int numeroLetras)
    {
        string guess = "a";
        do
        {
            guess = Console.ReadLine()?.ToUpper();

            foreach (string item in palabras)
            {
                if (guess == item)
                {
                    isTheGuessAPokemon = true;
                }
            }

            if (isTheGuessAPokemon == false)
            {
                //Imprime: Introduce un Pokémon de X letras y de la Pokédex que hayas seleccionado
                Console.WriteLine(textoJuego[0] + numeroLetras + " " + textoJuego[1]);
            }

        } while (isTheGuessAPokemon == false);

        return guess;

    }
    //Colorea la letra y el fondo
    void ColorJuego(string guess, int i, ConsoleColor colorFondo, ConsoleColor colorLetra)
    {
        Console.BackgroundColor = colorFondo;
        Console.ForegroundColor = colorLetra;
        Console.Write(guess[i]);
        Console.ResetColor();
    }
    //Comprueba la posición de las letras de guess
    void CheckGuess(string guess, string randomWord, int position)
    {

        for (int i = 0; i < guess.Length; i++)
        {
            //Por si la letra está en la posición correcta
            if (randomWord[i] == guess[i])
            {
                ColorJuego(guess, i, ConsoleColor.Green, ConsoleColor.Black);
            }
            //Si la letra no está en la posición correcta entonces llamará a esta otra función
            else if (randomWord[i] != guess[i])
            {
                IfNotRightPosition(guess, randomWord, position, i);
            }
        }
    }

    //Comprueba qué hacer en caso de que la letra no esté en la posición correcta.
    void IfNotRightPosition(string guess, string randomWord, int position, int i)
    {
        //Si la letra de la palabra que ha introducido el usuario se encuentra en la palabra oculta, position aumenta
        for (int j = 0; j < guess.Length; j++)
        {
            if (randomWord[j] == guess[i])
            { position++; }
        }
        //Si position es mayor que 0 significa que la letra está en la palabra oculta, pero no en la posición correcta.
        if (position != 0)
        {
            ColorJuego(guess, i, ConsoleColor.Yellow, ConsoleColor.Black);
            //Hacemos que position vuelva a ser 0 para que en el siguiente ciclo no se acumule su valor
            position = 0;
        }

        //Si position es igual a 0 significa que la letra no está en la palabra
        else
        {
            ColorJuego(guess, i, ConsoleColor.Red, ConsoleColor.Black);
        }
    }
    //Pregunta el nombre al usuario a imprime sus datos en un fichero con su nombre
    string AñadirNombre(string[] textoJuego, string intentosRestantes, string intentosTotales, string dificultad, string modo, string randomWord, string guess, string fullPathFiles)
    {

        Console.WriteLine(textoJuego[14]);//Imprime: Introduce tu nombre
        //Creamos una lista con los datos del jugador
        List<string> datosJugador = new List<string>();
        string nombreJugador = (Console.ReadLine());
        datosJugador.Add(nombreJugador);
        datosJugador.Add(dificultad);
        datosJugador.Add(modo);
        datosJugador.Add(intentosRestantes);
        datosJugador.Add(intentosTotales);
        datosJugador.Add(randomWord);
        //Seleccionamos el archivo con el nombre del jugador
        string datosJugadorFile = fullPathFiles + @"\Historial\" + datosJugador[0] + "_savedGame.txt";
        //En este fichero están enumerados las propiedades de los datos, como "Nombre:", "Dificultad:", etc.
        string datosHistorialFile = fullPathFiles + @"\Historial\DatosHistorial.txt";
        string[] datosHistorial = SplitRN(datosHistorialFile);

        //Si el archivo con el nombre no existe lo creamos
        if (!File.Exists(datosJugadorFile))
        {
            File.Create(datosJugadorFile).Close();
        }
        //Imprime: Datos registrados. Revisa el Historial para ver el registro
        Console.WriteLine(textoJuego[15]);

        //Escribimos el contenido de la lista en el fichero con el nombre del jugador.
        using (StreamWriter sw = File.AppendText(datosJugadorFile))
        {
            for (int i = 0; i < datosJugador.Count; i++)
            {
                sw.Write(datosHistorial[i]);
                sw.Write(datosJugador[i]);
                sw.WriteLine();
            }
            sw.WriteLine("_");

        }

        return nombreJugador;

    }

    //Selecciona la Pokédex con la que va a jugar el usuario. Si no selecciona nada jugará con primera a cuarta
    //generación por defecto
    int Modo(string fullPathFiles)
    {
        Console.Clear();
        string seleccionarModo = "a";
        int modo = 0;
        string textoJuegoFile = fullPathFiles + @"\Texto\TextoJuego.txt";
        string[] textoJuego = SplitRN(textoJuegoFile);

        do
        {
            ImprimirTitulo(fullPathFiles, @"\Titulos\Modo.txt");
            ImprimirTexto(fullPathFiles, @"\Modo\ModoTexto.txt");//Imprime los modos de juego

            seleccionarModo = Console.ReadLine();

            switch (seleccionarModo)
            {
                case "1":
                    modo = 1;
                    Console.Write(textoJuego[3]);
                    break;
                case "2":
                    modo = 2;
                    Console.Write(textoJuego[3]);
                    break;
                default:
                    Console.Write(textoJuego[4]);
                    Console.ReadLine();
                    break;
            }

        } while (modo == 0);

        return modo;
    }
    //Imprime el texto de las reglas
    void ImprimirReglas(string fullPathFiles, string ruta)
    {
        string reglasFile = fullPathFiles + ruta;
        string[] reglas = File.ReadAllLines(reglasFile);
        //Es la primera pantalla, donde le preguntamos al usuario si quiere leer las reglas. Luego empieza el juego

        for (int i = 0; i < reglas.Length; i++)
        {
            switch (i)
            {
                case 2:
                    Color(reglas[i], ConsoleColor.Green);
                    break;
                case 3:
                    Color(reglas[i], ConsoleColor.Yellow);
                    break;
                case 4:
                    Color(reglas[i], ConsoleColor.Red);
                    break;
                default:
                    Console.Write(reglas[i] + "\n\n");
                    break;

            }

        }
    }
    //Imprime las reglas
    void Reglas(string fullPathFiles)
    {
        Console.Clear();

        ImprimirTitulo(fullPathFiles, @"\Titulos\Reglas.txt");

        Console.WriteLine();

        ImprimirReglas(fullPathFiles, @"\Texto\ReglasTexto.txt");
    }
    //Imprime el contenido de la lista con el nombre del usuario 
    void Historial(string fullPathFiles, string nombreJugador)
    {
        Console.Clear();
        ImprimirTitulo(fullPathFiles, @"\Titulos\Historial.txt");

        string filePlayer = fullPathFiles + @"\Historial\" + nombreJugador + "_savedGame.txt";
        string textoJuegoFile = fullPathFiles + @"\Texto\TextoJuego.txt";
        string[] textoJuego = SplitRN(textoJuegoFile);

        if (File.Exists(filePlayer))
        {
            string datosJugador = File.ReadAllText(filePlayer);
            string[] datosJugadorSecciones = datosJugador.Split("_");

            for (int i = 0; i < datosJugadorSecciones.Length; i++)
            {
                Console.WriteLine(datosJugadorSecciones[i]);
            }
        }

        else
        {
            Console.WriteLine("\n" + textoJuego[17]); //Imprime: No hay ningún historial guardado. Primero juega una partida.
        }


    }
    //Sale del programa
    void Salir(string fullPathFiles)
    {
        Console.Clear();
        ImprimirTitulo(fullPathFiles, @"\Titulos\Salir.txt");
    }

    //Esto era código para colorear el historial, pero no sé cómo implementarlo. Se va a quedar aquí para estar guardado


    /**List<string> intentosRestantesList = new List<string>();
             List<string> intentosTotalesList = new List<string>();

             for (int i = 0; i < datosJugadorSecciones.Length; i++)
             {
                 string[] datosJugadorSplit = datosJugadorSecciones[i].Split("\r\n");
                 string datosJugadorIntentosRestantes = datosJugadorSplit[3].Split(":")[1];
                 string datosJugadorIntentosTotales = datosJugadorSplit[4].Split(":")[1];

                 string datosJug = File.ReadAllText(filePlayer);
                 string[] datosJugSec = datosJug.Split("_");

                 if (datosJugadorSplit[3].Contains(textoJuego[5]))
                 {
                     intentosRestantesList.Add(datosJugadorIntentosRestantes);
                 }
                 if (datosJugadorSplit[4].Contains(textoJuego[8]))
                 {
                     intentosTotalesList.Add(datosJugadorIntentosTotales);
                 }
             }**/

    /**string[] intentosRestantesString = intentosRestantesList.ToArray();
                string[] intentosTotalesString = intentosTotalesList.ToArray();
                int intentosRestantes = Convert.ToInt32(intentosRestantesString[i]);
                int intentosTotales = Convert.ToInt32(intentosTotalesString[i]);

                if (intentosRestantes >= (intentosTotales / 2))
                {
                    Color(datosJugadorSecciones[i], ConsoleColor.Green);
                }
                else if (intentosRestantes < (intentosTotales / 2) && intentosRestantes != 0)
                {
                    Color(datosJugadorSecciones[i], ConsoleColor.Yellow);
                }
                else if (intentosRestantes == 0)
                {
                    Color(datosJugadorSecciones[i], ConsoleColor.Red);
                }**/

}
