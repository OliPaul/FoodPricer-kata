namespace RCorpFoodPricer
{
    using System;

    enum FoodTypeEnum
    {
        Assiette,
        Sandwich
    }

    public enum DrinkSizeEnum
    {
        Petit,
        Moyen,
        Grand
    }

    public abstract class IFoodType
    {
        public abstract int calculatePrice(DrinkSizeEnum drinkSize, string dessertSize);
    }

    class FoodType
    {
        private IFoodType _foodType;

        public FoodType() {}

        public FoodType(IFoodType foodType)
        {
            _foodType = foodType;
        }

        public int calculatePrice(DrinkSizeEnum drinkSize, string dessertSize)
        {
            return _foodType.calculatePrice(drinkSize, dessertSize);
            
        }
        
        public int withCoffee(DrinkSizeEnum drinkSize, string dessertSize, string coffee)
        {
            int total = 0;
            if (drinkSize == DrinkSizeEnum.Moyen && dessertSize == "normal" && coffee == "yes")
            {
                Console.Write(" avec café offert!");
            }
            else
            {
                total += 1;
            }

            return total;
        }

        public FoodTypeEnum mapFoodType(string type)
        {
            return type == "assiette" ? FoodTypeEnum.Assiette : FoodTypeEnum.Sandwich;
        }
        
        public DrinkSizeEnum mapDrinkSize(string drinkSize)
        {

            if (drinkSize == "moyen")
            {
                return DrinkSizeEnum.Moyen;
            }
            if (drinkSize == "grand")
            {
                return DrinkSizeEnum.Grand;
            }
                
            return DrinkSizeEnum.Petit;
            
        }
    }

    class Assiette : IFoodType
    {
        private int total = 15;

        public override int calculatePrice(DrinkSizeEnum drinkSize, string dessertSize)
        {
            if (drinkSize == DrinkSizeEnum.Moyen && dessertSize == "normal")
            {
                Console.Write("Prix Formule Standard appliquée");
                total = 18;
            }
            else if (drinkSize == DrinkSizeEnum.Grand && dessertSize != "normal")
            {
                Console.Write("Prix Formule Max appliquée");
                total = 21;
            }
            else
            {
                withDrink(drinkSize);
                withDessert(dessertSize);
            }

            return total;
        }

        public void withDrink(DrinkSizeEnum drinkSizeEnum)
        {
            if (drinkSizeEnum == DrinkSizeEnum.Petit)
            {
                total += 2;
            }
            else if (drinkSizeEnum == DrinkSizeEnum.Moyen)
            {
                total += 3;
            }
            else
            {
                total += 4;
            }
        }

        public void withDessert(string dessertSize)
        {
            total += dessertSize == "normal" ? 2 : 4;
        }
    }

    class Sandwich : IFoodType
    {
        private int total = 10;

        public override int calculatePrice(DrinkSizeEnum drinkSize, string dessertSize)
        {
            if (drinkSize == DrinkSizeEnum.Moyen && dessertSize == "normal")
            {
                Console.Write("Prix Formule Standard appliquée");
                total = 13;
            }
            else if (drinkSize == DrinkSizeEnum.Grand && dessertSize != "normal")
            {
                Console.Write("Prix Formule Max appliquée");
                total = 16;
            }
            else
            {
                withDrink(drinkSize);
                withDessert(dessertSize);
            }

            return total;
        }

        public void withDrink(DrinkSizeEnum drinkSizeEnum)
        {
            if (drinkSizeEnum == DrinkSizeEnum.Petit)
            {
                total += 2;
            }
            else if (drinkSizeEnum == DrinkSizeEnum.Moyen)
            {
                total += 3;
            }
            else
            {
                total += 4;
            }
        }

        public void withDessert(string dessertSize)
        {
            total += dessertSize == "normal" ? 2 : 4;
        }
    }

    public class App
    {
        
        //calcule le prix payé par l'utilisateur dans le restaurant, en fonction de type de 
        //repas qu'il prend (assiette ou sandwich), de la taille de la boisson (petit, moyen, grand), du dessert et
        //de son type (normal ou special) et si il prend un café ou pas (yes ou no).
        //les prix sont fixes pour chaque type de chose mais des réductions peuvent s'appliquer
        //si cela rentre dans une formule!
        public double Compute(string type, string name, string beverage, string drinkSize, string dessert, string dessertSize, string coffee)
        {
            FoodType foodType = new FoodType();
            int total = 0;
            //le type ne peut être vide car le client doit déclarer au moins un repas
            if(string.IsNullOrEmpty(type+name)) return -1;

            FoodTypeEnum foodTypeEnum = foodType.mapFoodType(type);
            DrinkSizeEnum drinkSizeEnum = foodType.mapDrinkSize(drinkSize);

            if (foodTypeEnum == FoodTypeEnum.Assiette)
            {
                foodType = new FoodType(new Assiette());
            }
            else
            {
                foodType = new FoodType(new Sandwich());
            }

            total += foodType.calculatePrice(drinkSizeEnum, dessertSize);
            // Apply coffee discount 
            total += foodType.withCoffee(drinkSizeEnum, dessertSize, coffee);
            
            return total;
        }
    }
}