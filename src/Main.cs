using System;
using System.Collections.Generic;
using Gtk;

class Game
{
    public static Window mainWindow;
    
    public static List<ItemGroup> Inventory = new List<ItemGroup>();
    
    public static void Main()
    {
        Application.Init();
        
        mainWindow = new Window("Example Window");
        mainWindow.Resize(600, 400);
        
        HBox container = new HBox();
        ScrolledWindow consoleWrapper = new ScrolledWindow();
        TextView console = new TextView()
        {
            WrapMode = WrapMode.WordChar
        };
        //console.Editable = false;
        VBox buttonSeparator = new VBox();
        VButtonBox buttonBox = new VButtonBox();
        buttonBox.Layout = ButtonBoxStyle.Start;
        buttonSeparator.PackStart(buttonBox);
        Button quitButton = new Button("Quit");
        quitButton.Clicked += Exit;
        buttonSeparator.PackEnd(quitButton, false, false, 5);
        Button inventoryButton = new Button("Inventory");
        inventoryButton.Clicked += OpenInventory;
        Button roomButton = new Button("View Room"); 
        roomButton.Clicked += ViewRoom;
        buttonBox.PackStart(inventoryButton, false, false, 5);
        buttonBox.PackStart(roomButton, false, false, 5);
        
        consoleWrapper.Add(console);
        container.PackStart(consoleWrapper);
        container.PackStart(buttonSeparator, false, false, 5);
        
        ItemGroup testGroup = new ItemGroup();
        testGroup.Items.Add(new Item() { Name = "Testing Item", Description = "A testing item. Has no practical use." });
        testGroup.Items.Add(new Item() { Name = "Testing Item", Description = "A testing iten, Has no practical use." });
        
        Inventory.Add(testGroup);
        
        mainWindow.Add(container);
        mainWindow.ShowAll();
        
        mainWindow.DeleteEvent += Quit;
        
        Application.Run();
    }
    
    static void ViewRoom(object obj, EventArgs args)
    {
        /*Window roomWindow = new Window("Room");
        roomWindow.Resize(400, 300);
        
        VBox container = new VBox();
        HButtonBox categoryButtonBox = new HButtonBox();
        container.PackStart(categoryButtonBox, false, false, 5);
        
        Button itemButton = new Button("Items");
        Button creatureButton = new Button("Creatures");
        Button otherButton = new Button("Other");
        categoryButtonBox.PackStart(itemToggle);
        categoryButtonBox.PackStart(creatureToggle);
        categoryButtonBox.PackStart(otherToggle);
        roomWindow.Add(container);
        roomWindow.ShowAll();*/
    }
    
    static void OpenInventory(object obj, EventArgs args)
    {
        InventoryWindow hello = new InventoryWindow("Inventory");
    }
    
    static void Quit(object obj, DeleteEventArgs args)
    {
        Application.Quit();
    }
    
    static void Exit(object obj, EventArgs args)
    {
        Application.Quit();
    }
}