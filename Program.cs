using System.Reflection.PortableExecutable;

class Program
{
    static void Main()
    {
        List<Character> party = new List<Character>(); 
        getParty(party);
        
        int choice;

        do
        {
            switch (choice = menu())
            {
                case 1: displayPartyFullInfo(party); break;
                case 2: party.Add(addCharacter()); break;
                case 3: int update = displayPartyNames(party); levelUp(party, update); break;
            }
        } while (choice != 4);

        updateFile(party);
    }







    public static int menu()
    {
        Console.Write("\n--------------------\n" +
                            "1. Display Characters\n" +
                            "2. Add Character\n" +
                            "3. Level Up Character\n" +
                            "4. Exit\n\n" +
                            "Select an option: ");
        int choice = int.Parse(Console.ReadLine());

        while ((choice < 1) || (choice > 4))
        {
            Console.Write("Invalid option selected. Try again: ");
            choice = int.Parse(Console.ReadLine());
        }
        Console.WriteLine("--------------------\n"); //spacing

        return choice;
    }

    static public void getParty(List<Character> party)
    {
       string path = "../../../input.csv";
        StreamReader reader = new StreamReader(path);

        string line;
        Character inputCharacter;
        while ((line = reader.ReadLine()) != null)
        {
            String[] charLine = line.Split(',');
            inputCharacter = new Character();
            inputCharacter.Name = charLine[0];
            inputCharacter.Class = charLine[1];
            inputCharacter.Level = int.Parse(charLine[2]);
            inputCharacter.Health = int.Parse(charLine[3]);
            inputCharacter.Equipment = charLine[4].Split('|');

            party.Add(inputCharacter);
        }

        reader.Close();
    }

    static public void displayPartyFullInfo(List<Character> party)
    {
        for (int i = 0; i < party.Count; i++)
            Console.WriteLine($"{i + 1}: {party[i].ToString()}");
    }

    static public Character addCharacter()
    {
        Character newCharacter = new Character();

        Console.Write("Enter your character's name: ");
        newCharacter.Name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        newCharacter.Class = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        newCharacter.Level = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's HP: ");
        newCharacter.Health = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        newCharacter.Equipment = Console.ReadLine().Split('|');

        return newCharacter;
    }

    static public int displayPartyNames(List<Character> party)
    {
        for (int i = 0; i < party.Count; i++)
            Console.WriteLine($"{i + 1}: {party[i].Name}");

        Console.Write("\nWhich character would you like to level up? ");
        int choice = int.Parse(Console.ReadLine());

        while ((choice < 1) || (choice > party.Count))
        {
            Console.Write("Invalid selection. Try again: ");
            choice = int.Parse(Console.ReadLine());
        }

        return choice - 1;
    }

    static public void levelUp(List<Character> party, int option)
    {
        Console.WriteLine($"\nYou are now leveling up {party[option].Name}!");

        Console.Write($"Please enter {party[option].Name}'s new level: ");
        party[option].Level = int.Parse(Console.ReadLine());

        Console.Write($"Please enter {party[option].Name}'s new HP: ");
        party[option].Health = int.Parse(Console.ReadLine());

        Console.WriteLine($"{party[option].Name} has been successfully leveled up!");
    }

    static public void updateFile(List<Character> party)
    {
        string path = "../../../input.csv";

        StreamWriter outputFile = new StreamWriter(path, false);

        foreach (Character c in party)
            outputFile.WriteLine(c.csvOutput());

        outputFile.Close();
    }

    public class Character
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public string[] Equipment { get; set; }

        public override string ToString()
        {
            return $"{this.Name}──Level {this.Level} {this.Class}. HP: {this.Health}. Equipment: {string.Join(", ", this.Equipment)}.";
        }

        public string csvOutput()
        {
            return $"{this.Name},{this.Class},{this.Level},{this.Health},{string.Join("|", this.Equipment)}";
        }
    }
}