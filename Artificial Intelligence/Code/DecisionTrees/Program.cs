
using DecisionTrees;
using System.Globalization;

// Zorg ervoor dat getallen met een punt verschijnen (0.5).
CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

// ============================================================
// DATA
// ============================================================

// Binge-watchen (cursus 6.3.3)
// Genre: 0 = Drama, 1 = SF
// Tempo: 0 = Traag, 1 = Snel

var binge = new List<Sample>
{
    new([1, 1], "Binge"),       // 1. SF, snel
    new([1, 1], "Binge"),       // 2. SF, snel
    new([1, 0], "Binge"),       // 3. SF, traag
    new([1, 0], "Geen binge"),  // 4. SF, traag
    new([0, 1], "Binge"),       // 5. Drama, snel
    new([0, 1], "Geen binge"), // 6. Drama, snel
    new([0, 0], "Geen binge"), // 7. Drama, traag
    new([0, 0], "Geen binge")  // 8. Drama, traag
};

// Klanten uit les 5
// Kenmerken: leeftijd, kijkminuten per week

var klanten = new List<Sample>
{
    new([19, 520], "actie"),      // Lotte
    new([21, 260], "actie"),      // Emma
    new([27, 640], "actie"),      // Yusuf
    new([47, 440], "romantiek"),  // Marc
    new([58, 410], "romantiek"),  // Rita
    new([52, 390], "romantiek")   // Hilde
};

// ============================================================
// BLOK 1 - GINI
// ============================================================

Console.WriteLine("===== BLOK 1 - Gini =====");

Check("Groep A: alle 8 (4 - 4)",
    DecisionTree.Gini(binge), "0.5");

Check("Groep B: SF (3 - 1)",
    DecisionTree.Gini(binge.Take(4).ToList()), "0.375");

Check("Groep C: zuiver (2 - 0)",
    DecisionTree.Gini(binge.Take(2).ToList()), "0");

// ============================================================
// BLOK 2 - DE BESTE VRAAG
// ============================================================

Console.WriteLine("\n===== BLOK 2 - De beste vraag =====");

Check("Winst Genre",
    DecisionTree.GiniGain(binge, 0, 0.5), "0.125");

Check("Winst Tempo",
    DecisionTree.GiniGain(binge, 1, 0.5), "0.125");

Check("Winst leeftijd <= 24",
    DecisionTree.GiniGain(klanten, 0, 24), "0.25");

Check("Winst leeftijd <= 37",
    DecisionTree.GiniGain(klanten, 0, 37), "0.5");

// Lege boom om BestSplit te testen
var leeg = new DecisionTree(new[] { "x", "y" });

CheckSplit("Beste vraag binge",
    leeg.BestSplit(binge), "(0, 0.5, 0.125)");

CheckSplit("Beste vraag klanten",
    leeg.BestSplit(klanten), "(0, 37, 0.5)");

// ============================================================
// BLOK 3 - DE BOOM BOUWEN
// ============================================================

Console.WriteLine("\n===== BLOK 3 - De boom bouwen =====");

// Beslissingsboom voor klanten
var boomKlanten = new DecisionTree(
    new[] { "Leeftijd", "Minuten" },
    maxDepth: 3
);

boomKlanten.Train(klanten);
boomKlanten.Print();

Console.WriteLine(
    $"Nieuwe klant (24 jaar, 420 min) --> " +
    $"{boomKlanten.Predict([24, 420])}   verwacht: actie\n"
);

// Beslissingsboom voor binge-watchen
var boomBinge = new DecisionTree(
    new[] { "Genre", "Tempo" },
    maxDepth: 2
);

boomBinge.Train(binge);
boomBinge.Print();

// ============================================================
// BLOK 4 - HOE DIEP?
// ============================================================

Console.WriteLine("\n===== BLOK 4 - Hoe diep? =====");

// Iris-dataset inladen
List<Sample> iris = LoadCsv("iris.csv");

// Elke derde bloem gebruiken om te testen
var test = iris
    .Where((s, i) => i % 3 == 0)
    .ToList();

// De overige bloemen gebruiken om te trainen
var train = iris
    .Where((s, i) => i % 3 != 0)
    .ToList();

// Kenmerken van de bloemen
string[] irisKenmerken =
{
    "kelkblad_lengte",
    "kelkblad_breedte",
    "kroonblad_lengte",
    "kroonblad_breedte"
};

// De bloem uit les 5 (kNN: virginica)
double[] bloem = { 6.1, 2.8, 4.9, 1.7 };

// Verschillende boomdieptes vergelijken
Console.WriteLine("maxDepth  train   test   bloem uit les 5");

for (int d = 1; d <= 6; d++)
{
    var boom = new DecisionTree(
        irisKenmerken,
        maxDepth: d
    );

    boom.Train(train);

    Console.WriteLine(
        $"{d,8}  " +
        $"{Accuracy(boom, train),5:P0}  " +
        $"{Accuracy(boom, test),5:P0}   " +
        $"{boom.Predict(bloem)}"
    );
}

// ============================================================
// HULPFUNCTIES - NIET AANPASSEN
// ============================================================

static void Check(string wat, double resultaat, string verwacht) =>
    Console.WriteLine(
        $"{wat,-28} {resultaat:0.###}   verwacht: {verwacht}"
    );

static void CheckSplit(
    string wat,
    (int Feature, double Threshold, double Gain) s,
    string verwacht
) =>
    Console.WriteLine(
        $"{wat,-28} " +
        $"({s.Feature}, {s.Threshold:0.##}, {s.Gain:0.###})" +
        $"   verwacht: {verwacht}"
    );

static double Accuracy(DecisionTree boom, List<Sample> data) =>
    (double)data.Count(s => boom.Predict(s.Features) == s.Label)
    / data.Count;

static List<Sample> LoadCsv(string pad)
{
    var data = new List<Sample>();

    foreach (string lijn in File.ReadLines(pad).Skip(1))
    {
        string[] delen = lijn.Split(',');

        double[] kenmerken = delen[..^1]
            .Select(v => double.Parse(v, CultureInfo.InvariantCulture))
            .ToArray();

        data.Add(new Sample(kenmerken, delen[^1]));
    }

    return data;
}

