using FluentAssertions;
using GameProject;

namespace GamesTests;

public class UnitTest1
{

    [Fact]
    public void TestGetDlc()
    {
        Game eldenRing = new Game("Elden Ring", " ");
        var shadow = eldenRing.AddDLC("Shadow of The Erdtree", " ");

        eldenRing.GetAllDlc().Should().HaveCount(1);
        shadow.GetAllDlc().Should().BeEmpty();
    }

    [Fact]
    public void TestIsDlc()
    {
        Game eldenRing = new Game("Elden Ring", " ");
        var shadow = eldenRing.AddDLC("Shadow of The Erdtree", " ");

        eldenRing.IsDLc.Should().BeFalse();
        shadow.IsDLc.Should().BeTrue();
    }



    [Fact]
    public void TestErrorDlc()
    {

        Game eldenRing = new Game("Elden Ring", " ");
        var shadow = eldenRing.AddDLC("Shadow of The Erdtree", " ");

        Action action = () => shadow.AddDLC("Elden ring", "skaks");
        action.Should().Throw<InvalidOperationException>();

        Action action1 = () => eldenRing.AddDLC("Shadow of The Erdtree", " ");
        action1.Should().Throw<DlcAlreadyAddedException>();


    }
    [Fact]

    public void TestSplit()
    {


        string[] lines =
[ "----------------------------------------------------" ,
"Title: Breath OF The Wild                           " ,
"Description: jiqml                                  " ,
"Platform: NSW                                       " ,
"Platform Desription: Nintendo Switch                " ,
"Store: AMAZON                                       " ,
"store description: amazon kkj                       " ,
"Store link: www.amazonitalia                        " ,
"Media Format: Phisycal                              " ,
"Launcher: nsw                                       " ,
"Launcher description: aaaa                          " ,
"Launcher Link: link                                 " ,
"Aquired date: 2022 - 03 - 07                        " ,
"Price: 49.99                                        " ,
"----------------------------------------------------" ,
"Title: Metroid Prime                                " ,
"Description: jiqml                                  " ,
"Platform: NSW                                       " ,
"Store: eshop                                        " ,
"store description: nintendo kkj                     " ,
"Store link: www.nintendoitalia                      " ,
"Media Format: digital                               " ,
"Launcher: nsw                                       " ,
"Aquired date: 2021 - 03 - 07                        " ,
"Price: 39.99                                        " ];


        bool Separator(string s) =>
           (s.StartsWith("-----------"));
        var splitResult = lines.SplitLines(Separator, s => string.IsNullOrWhiteSpace(s) || s.Trim().StartsWith("#"));
        splitResult.Should().HaveCount(2);
        splitResult[0][0].Should().Be(lines[1]);
        splitResult.Last().Last().Should().Be(lines.Last());

    }

    [Fact]

    public void TestSplitWithAllSeparator()
    {


        string[] lines =
[
"----------------------------------------------------" ,
    "----------------------------------------------------" ,
    "----------------------------------------------------",
    "----------------------------------------------------"];


        bool Separator(string s) =>
           (s.StartsWith("-----------"));
        var splitResult = lines.SplitLines(Separator, string.IsNullOrWhiteSpace);
        splitResult.Should().HaveCount(0);


    }


    [Fact]

    public void TestSplitWithAllSpace()
    {
        string[] lines =
[
    "",
    "",
    "",
    "",
        ];
        bool Separator(string s) =>
           (s.StartsWith("-----------"));
        var splitResult = lines.SplitLines(Separator, string.IsNullOrWhiteSpace);
        splitResult.Should().HaveCount(0);
    }


    [Fact]

    public void TestGameDtoNormal()
    {

        string[] lines =
        [
        "Title: Breath OF The Wild                           " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Phisycal                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
 ];


        GameImportDto gameDto = new GameImportDto();
        gameDto = lines.ToGameImportDto();
        gameDto.Title.Should().Be("Breath OF The Wild");
        gameDto.Description.Should().Be("jiqml");
        gameDto.Platform.Should().Be("NSW");
        gameDto.PlatformDescription.Should().Be("Nintendo Switch");
        gameDto.Store.Should().Be("AMAZON");
        gameDto.StoreDescription.Should().Be("amazon kkj");
        gameDto.StoreLink.Should().Be("https://www.amazon.it");
        gameDto.MediaFormat.Should().Be("Phisycal");
        gameDto.Launcher.Should().Be("nsw");
        gameDto.LauncherDescription.Should().Be("aaaa");
        gameDto.LauncherLink.Should().Be("https://www.amazon.it ");
        gameDto.AquiredDate.Should().Be(new DateOnly(2022, 03, 07));
        gameDto.Price.Should().Be(49.99m);



    }

    [Fact]
    public void TestGameDtoWithTitleErrors()
    {

        string[] lines =
        [
     "21wsq: Breath OF The Wild    " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Phisycal                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
 ];

        GameImportDto gameDto = new GameImportDto();
        Action action = () => lines.ToGameImportDto();
        action.Should().Throw<GameImportException>();


    }

    [Fact]
    public void TestImportdataFroExternalFile()
    {
        string[] lines =
       [
    "----------------------------------------------------" ,
    "Title: Breath OF The Wild    " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
    "Title: Metroid   " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
    "Title:Breath Of The Wild: Expansion pass",
    "Main Title:Breath Of The Wild",
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,

 ];
        GameTransactionsRepository test = new GameTransactionsRepository(new List<GameTransaction>());
        Action action = () => test.ImportdataFroExternalFile(lines);
        action.Should().NotThrow<Exception>();
    }

    [Fact]
    public void TestImportdataFroExternalWithMainGameNotFoundException()
    {
        string[] lines =
[
    "----------------------------------------------------" ,
    "Title: Metroid   " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
    "Title:Breath Of The Wild: Expansion pass",
    "Main Title:Breath Of The Wild",
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
 ];
        GameTransactionsRepository test = new GameTransactionsRepository(new List<GameTransaction>());
        Action action = () => test.ImportdataFroExternalFile(lines);
        action.Should().Throw<MainGameNotFoundException>();
    }

    [Fact]
    public void TestImportdataFroExternalWithSameDlcError()
    {
        string[] lines =
[
"----------------------------------------------------" ,
    "Title: Breath OF The Wild    " ,
    "Description: jiqml                                  " ,
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
    "Title:Breath Of The Wild: Expansion pass",
    "Main Title:Breath Of The Wild",
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                              " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,
    "----------------------------------------------------" ,
    "Title:Breath Of The Wild: Expansion pass",
    "Main Title:Breath Of The Wild",
    "Platform: NSW                                       " ,
    "Platform Description: Nintendo Switch                " ,
    "Store: AMAZON                                       " ,
    "store description: amazon kkj " ,
    "Store link: https://www.amazon.it                        " ,
    "Media Format: Physical                             " ,
    "Launcher: nsw                                       " ,
    "Launcher description: aaaa                          " ,
    "Launcher Link: https://www.amazon.it                               " ,
    "Aquired date: 2022-03-07" ,
    "Price: 49.99                                        "  ,

 ];
        GameTransactionsRepository test = new GameTransactionsRepository(new List<GameTransaction>());
        Action action = () => test.ImportdataFroExternalFile(lines);
        action.Should().Throw<DlcAlreadyAddedException>();
    }

    [Fact]
    public void TestImportdataFroExternalNumberTest()
    {
        GameTransactionsRepository test = new GameTransactionsRepository(new List<GameTransaction>());
        test.ImportdataFroExternalFile("./GameList.txt");
        test.GameTransactionsList.Should().HaveCount(3);
    }
}
    








