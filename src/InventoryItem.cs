using System;
using System.Collections.Generic;
using Gtk;

public class InventoryWindow : Gtk.Window
{
    public InventoryWindow(string name) : base(name)
    {
        build();
    }
    
    private void build()
    {
        Resize(400, 300);
        
        ScrolledWindow wrapper = new ScrolledWindow();
        Viewport viewport = new Viewport();
        wrapper.Add(viewport);
        VBox itemList = new VBox();
        viewport.Add(itemList);
        for (int i = 0; i < Game.Inventory.Count; i++)
        {
            itemList.PackStart(new InventoryItemGroup(Game.Inventory[i], this), false, false, 0);
        }
        Add(wrapper);
        ShowAll();
    }
}

public class InventoryItemGroup : Gtk.Bin
{
    ItemGroup itemGroup;
    InventoryWindow parent;
    
    public InventoryItemGroup(ItemGroup ig, InventoryWindow parent) : base()
    {
        this.parent = parent;
        build(ig);
    }
    
    private void build(ItemGroup ig)
    {
        itemGroup = ig;
        HBox container = new HBox();
        Label countLabel = new Label(ig.Count.ToString());
        Label nameLabel = new Label(ig.Name);
        Button infoButton = new Button(" i ");
        infoButton.Clicked += GetItemInfo;
        Button dropButton = new Button("Drop");
        dropButton.Clicked += DropItem;
        container.PackStart(countLabel, false, false, 0);
        container.PackStart(nameLabel, true, false, 0);
        container.PackStart(dropButton, false, false, 5);
        container.PackStart(infoButton, false, false, 5);
        Add(container);
    }
    
    protected override void OnSizeAllocated(Gdk.Rectangle allocation)
    {
        if (Child != null)
        {
            Child.Allocation = allocation;
        }
    }
    
    protected override void OnSizeRequested(ref Requisition requisition)
    {
        if (Child != null)
        {
            requisition = Child.SizeRequest();
        }
    }
    
    private void GetItemInfo(object obj, EventArgs args)
    {
        Window itemInfo = new Window(itemGroup.Name);
        VBox lv = new VBox();
        Label lbl = new Label(itemGroup.Description);
        lv.Add(lbl);
        itemInfo.Add(lv);
        itemInfo.ShowAll();
    }
    
    private void DropItem(object obj, EventArgs args)
    {
        if (itemGroup.Count > 1)
        {
            Dialog dialog = new Dialog("Drop Item", parent, Gtk.DialogFlags.DestroyWithParent)
            {
                 Modal = true,
                 HasSeparator = true
            };
            NumericalEntry entry = new NumericalEntry();
            Label query = new Label("Please input the amount to drop.");
            dialog.VBox.PackStart(query);
            dialog.VBox.PackStart(entry);
            query.Show();
            entry.Show();
            dialog.AddButton("Accept", ResponseType.Accept);
            dialog.AddButton("Close", ResponseType.Close);
            dialog.Run();
            dialog.Destroy();
        }
        else
        {
            Game.Inventory.Remove(itemGroup);
            Destroy();
        }
        
    }
}

public class NumericalEntry : Entry
{
    public NumericalEntry() : base()
    {
    
    }
    
    protected override void OnTextInserted(string text, ref int position)
    {
        try
        {
            int.Parse(text);
            base.OnTextInserted(text, ref position);
        }
        catch
        {
            
        }
    }
}

public class ItemGroup
{
    List<Item> items = new List<Item>();
    
    public List<Item> Items { get => items; set => items = value; }
    public string Name { get => items[0].Name; }
    public int Count { get => items.Count; }
    public string Description { get => items[0].Description; }
}

public class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
}