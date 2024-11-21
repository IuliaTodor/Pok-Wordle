using Wordle;

namespace WordleUnitTest
{
    public class Tests
    {
        [Test]
        public void TestMethodSplitRN()
        {
            string path = Path.GetFullPath(@"C:\Users\Colli\source\repos\");
            string fullPathFiles = path + @"Wordle_todorIulia\";
            string[] array = { "hola", "esto es un test" };
            Assert.That(Wordle.Wordle.SplitRN(fullPathFiles + @"FilesForUnitTest\TestForSplitRN.txt"), Is.EqualTo(array));
        }

        [Test]
        public void TestMethodSplitTwoDots()
        {
            List<string> list = new List<string>();
            string linea = "hola:esto es un test";
            list.Add(linea);
            string[] array = { "hola", "esto es un test" };
            Assert.That(Wordle.Wordle.SplitTwoDots(list, 0), Is.EqualTo(array));
        }
        [Test]
        public void TestMethodGetMode()
        {    
            string path = Path.GetFullPath(@"C:\Users\Colli\source\repos\");
            string fullPathFiles = path + @"Wordle_todorIulia\";
            string enumerarModosFile = fullPathFiles + @"Modo\EnumerarModos.txt";
            List<string> listaModos = new List<string>();
            string[] defaultGen = {"Default", "Pokemon1-4Gen"};
            string[] P14 = { "1", "Pokemon1-4Gen" };
            string[] P59 = { "2", "Pokemon5-9Gen" };
            int selectorModo = 0;
            string modo = defaultGen[1];
            Assert.That(Wordle.Wordle.GetMode(selectorModo, modo, defaultGen, P14, P59), Is.EqualTo(modo));
        }


        [Test]
        public void TestMethodChargeFilePokemon14GenFacil()
        {
            string path = Path.GetFullPath(@"C:\Users\Colli\source\repos\");
            string fullPathFiles = path + @"Wordle_todorIulia\";
            string[] palabras = Wordle.Wordle.SplitRN(fullPathFiles + @"Palabras\Pokemon1-4GenFacil.txt");
            string dificultades = "Facil";
            string stringModo = "Pokemon1-4Gen";

            Assert.That(Wordle.Wordle.ChargeFile(fullPathFiles, palabras, dificultades, stringModo), Is.EqualTo(palabras));
        }

        [Test]
        public void TestMethodCheckMode()
        {
            string path = Path.GetFullPath(@"C:\Users\Colli\source\repos\");
            string fullPathFiles = path + @"Wordle_todorIulia\";
            string[] palabras = Wordle.Wordle.SplitRN(fullPathFiles + @"Palabras\Pokemon1-4GenFacil.txt");
            int selectorModo = 1;
            string[] defaultGen = { "Default", "Pokemon1-4Gen" };
            string[] P14 = { "1", "Pokemon1-4Gen" };
            string[] P59 = { "2", "Pokemon5-9Gen" };
            string dificultades = "Facil";
 
            Assert.That(Wordle.Wordle.CheckMode(selectorModo, palabras, fullPathFiles,dificultades, defaultGen, P14, P59), Is.EqualTo(palabras));
        }

    }
}