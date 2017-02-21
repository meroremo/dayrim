using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemManager 
{
	
	public ArrayList inventar;// = new ArrayList();
    private CanvasNavigator navi;
    private ItemCombinations combiVar;

    public ItemManager()
	{
		inventar = new ArrayList();

        navi = new CanvasNavigator();
        combiVar = new ItemCombinations();
	}
		
	public void addItem(Item i)
	{
		inventar.Add (i);

	}

	//public void Replace(Item i, int count)
	//{
	//	inventar.
	//}

	public void deleteItem (Item i)
	{
		inventar.Remove (i);

	}

	public Item getItem(int i)
	{
		return (Item) inventar[i];
	}

    public int getAnzahlItems()
    {
        int i = 0;

        foreach (Item o  in inventar)
        {
            i++;
        }

        return i;
    }


    public ArrayList getInventar()
    {
        ArrayList itemList = inventar;
        return (ArrayList) itemList;
    }


    public void possibleItemCombinations(Item i1, Item i2)
	{
        //test einer objektkombi
        // z.b. abfrage der namen
        navi.deleteAllItems();

        // Roter Ball = Gummiball + Dose
        if (i1.getName () == "Gummiball" && i2.getName () == "Spruehdose" || i1.getName () == "Spruehdose" && i2.getName () == "Gummiball") {

            searchItem("Gummiball");

           
            Sprite s = Resources.Load<Sprite>("RoterBall");
            Item newItem = new Item("RoterBall", "Oh weh, oh je... ! Das sieht aber schon ziemlich lecker aus. Aber irgendwas fehlt noch...hmm", 8, s);
            inventar.Add (newItem);
		}

		// Ballpflanze(Zwischenstand A) = Gummiball + Plastikpflanze
		else if (i1.getName () == "Gummiball" && i2.getName () == "Gummipflanze" || i1.getName () == "Gummipflanze" && i2.getName () == "Gummiball") {


				inventar.Remove (i1);
				inventar.Remove (i2);
			
			inventar.Add (new Item ("Ballpflanze", "Zwischenstand A", 3, new Sprite ()));
		}

		// Pfirsichtomate =  Ballpflanze + Dose
		else if (i1.getName () == "Spruehdose" && i2.getName () == "Ballpflanze" || i1.getName () == "Ballpflanze" && i2.getName () == "Spruehdose") {


			if (i1.getName () == "Ballpflanze") {
				inventar.Remove (i1);
			} else {
				inventar.Remove (i2);
			}

			inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 5, new Sprite()));
		}
		// Pfirsichtomate = Roter Ball + Plastikpflanze
		else if (i1.getName () == "RoterBall" && i2.getName () == "Gummipflanze" || i1.getName () == "Gummipflanze" && i2.getName () == "RoterBall") 
		{

            searchItem("Gummipflanze");
            searchItem("RoterBall");

            Sprite s = Resources.Load<Sprite>("Pfirsischtomate");
            Item newItem = new Item("Pfirsichtomate", "Das ist mal eine saftig lackierte Gummiball-Pfirsichtomate wie sie im Buche steht!", 5, s);
            inventar.Add(newItem);
        }



        // WIR MALEN DIE ROSEN ROT
        else if (i1.getName() == "Spruehdose" && i2.getName() == "Gummipflanze" || i1.getName() == "Gummipflanze" && i2.getName() == "Spruehdose")
        {
           // navi.deleteAllItems();

            searchItem("Spruehdose");
            searchItem("Gummipflanze");

            /*for (int i = 0; i < TouchInputManager.manager.getAnzahlItems(); i++)
            {
                 if (TouchInputManager.manager.getItem(i).getName() == "Spruehdose")
                {
                    TouchInputManager.manager.getInventar().RemoveAt(i);
                }
            }
            for (int i = 0; i < TouchInputManager.manager.getAnzahlItems(); i++)
            {
                if (TouchInputManager.manager.getItem(i).getName() == "Gummipflanze")
                {
                    TouchInputManager.manager.getInventar().RemoveAt(i);
                }
            }*/

            ItemCombinations.isRemoved = true;
            //_________________________________________________________________!!!!!!!!!!!WICHTIG!!!!!!!!!!!!!!!_______________ZU BEAcHTEN BEI ANDEREN COMBIS

            //navi.setAllItems();

            //inventar.Add(new Item("rote Rosen", "Zwischenstand Blödsinn", 100, new Sprite()));
        }


        navi.setAllItems();

        /*if (i1.getName() == "Gummiball" || i2.getName () == "Gummiball")
		{
			if (i1.getName () == "Gummiball" && i2.getName () == "Dose") 
			{
				if (i1.getName () == "Gummiball") {
					inventar.Remove (i1);
				} else 
				{
					inventar.Remove (i2);
				}
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}
			else if (i2.getName () == "Gummiball" && i1.getName () == "Dose")
			{
				inventar.Remove (i1);
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}
		}
		else if (i1.getName() == "Dose" || i2.getName () == "Dose")
		{
			if (i1.getName () == "Gummiball" && i2.getName () == "Dose") 
			{
				inventar.Remove (i2);
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}
			else if (i2.getName () == "Gummiball" && i1.getName () == "Dose")
			{
				inventar.Remove (i2);
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}
		}


		else if(i1.getName() == "Pfirsichtomate" || i2.getName () == "Pfirsichtomate")
		{
			if (i1.getName () == "Pfirsichtomate" && i2.getName () == "Dose") 
			{
				inventar.Remove (i1);
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}
			else if (i2.getName () == "Pfirsichtomate" && i1.getName () == "Dose")
			{
				inventar.Remove (i1);
				inventar.Add (new Item("Pfirsichtomate", "Saftig lackierte Gummiball-Pfirsichtomate", 3, new Sprite()));
			}	
			}

*/

    }
	

    void searchItem(string item)
    {
        for (int i = 0; i < TouchInputManager.manager.getAnzahlItems(); i++)
        {
            if (TouchInputManager.manager.getItem(i).getName() == item)
            {
                TouchInputManager.manager.getInventar().RemoveAt(i);
            }
        }
    }
}
