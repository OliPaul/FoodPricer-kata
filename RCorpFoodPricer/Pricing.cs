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

    public class Menu
    {
        private IFoodType _foodType;
        private DrinkSizeEnum _drinkSize;
        private string _dessertSize;

        public IFoodType FoodType
        {
            get => _foodType;
            set => _foodType = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DrinkSizeEnum DrinkSize
        {
            get => _drinkSize;
            set => _drinkSize = value;
        }

        public string DessertSize
        {
            get => _dessertSize;
            set => _dessertSize = value ?? throw new ArgumentNullException(nameof(value));
        }
    }

    public interface IFoodType
    {
        int calculatePrice(string size, string dsize);
    }

    class FoodType
    {
        private IFoodType _foodType;

        public FoodType() {}

        public FoodType(IFoodType foodType)
        {
            _foodType = foodType;
        }

        public int calculatePrice(string size, string dsize)
        {
            return _foodType.calculatePrice(size, dsize);
        }

        public FoodTypeEnum mapFoodType(string type)
        {
            return type == "assiette" ? FoodTypeEnum.Assiette : FoodTypeEnum.Sandwich;
        }
    }

    class Assiette : IFoodType
    {
        public int calculatePrice(string size, string dsize)
        {
            int total = 15;
            //ainsi qu'une boisson de taille:
            switch(size)
            {
                case "petit": 
                    total+=2;
                    //dans ce cas, on applique la formule standard
                    if(dsize=="normal")
                    {
                        //pas de formule
                        //on ajoute le prix du dessert normal
                        total+=2;
                    } else {
                        //sinon on rajoute le prix du dessert special
                        total+=4;
                    }
                    break;
                //si on prends moyen
                case "moyen": 
                    total+=3;
                    //dans ce cas, on applique la formule standard
                    if(dsize=="normal")
                    {
                        //j'affiche la formule appliquée
                        Console.Write("Prix Formule Standard appliquée ");
                        //le prix de la formule est donc 18
                        total=18;
                    } else {
                        //sinon on rajoute le prix du dessert special
                        total+=4;
                    }
                    break;
                case "grand": 
                    total+=4;
                    //dans ce cas, on applique la formule standard
                    if(dsize=="normal")
                    {
                        //pas de formule
                        //on ajoute le prix du dessert normal
                        total+=2;
                    } else {
                        //dans ce cas on a la fomule max
                        Console.Write("Prix Formule Max appliquée ");
                        total=21;
                    }
                    break;
            }

            return total;
        }
    }

    class Sandwich : IFoodType
    {
        public int calculatePrice(string size, string dsize)
        {
            int total = 10;
            //ainsi qu'une boisson de taille:
            switch(size)
                {
                    case "petit": 
                        total+=2;
                        //dans ce cas, on applique la formule standard
                        if(dsize=="normal")
                        {
                            //pas de formule
                            //on ajoute le prix du dessert normal
                            total+=2;
                        } else {
                            //sinon on rajoute le prix du dessert special
                            total+=4;
                        }
                        break;
                    //si on prends moyen
                    case "moyen": 
                        total+=3;
                        //dans ce cas, on applique la formule standard
                        if(dsize=="normal")
                        {
                            //j'affiche la formule appliquée
                            Console.Write("Prix Formule Standard appliquée ");
                            //le prix de la formule est donc 18
                            total=13;
                        } else {
                            //sinon on rajoute le prix du dessert special
                            total+=4;
                        }
                        break;
                    case "grand": 
                        total+=4;
                        //dans ce cas, on applique la formule standard
                        if(dsize=="normal")
                        {
                            //pas de formule
                            //on ajoute le prix du dessert normal
                            total+=2;
                        } else {
                            //dans ce cas on a la fomule max
                            Console.Write("Prix Formule Max appliquée ");
                            total=16;
                        }
                        break;
                }
            
            return total;
        }
    }

    public class App
    {
        
        //calcule le prix payé par l'utilisateur dans le restaurant, en fonction de type de 
        //repas qu'il prend (assiette ou sandwich), de la taille de la boisson (petit, moyen, grand), du dessert et
        //de son type (normal ou special) et si il prend un café ou pas (yes ou no).
        //les prix sont fixes pour chaque type de chose mais des réductions peuvent s'appliquer
        //si cela rentre dans une formule!
        public double Compute(string type, string name, string beverage, string size, string dessert, string dsize, string coffee)
        {
            FoodType foodType = new FoodType();
            int total = 0;
            //le type ne peut être vide car le client doit déclarer au moins un repas
            if(string.IsNullOrEmpty(type+name)) return -1;

            FoodTypeEnum foodTypeEnum = foodType.mapFoodType(type);

            if (foodTypeEnum == FoodTypeEnum.Assiette)
            {
                foodType = new FoodType(new Assiette());
            }
            else
            {
                foodType = new FoodType(new Sandwich());
            }

            total += foodType.calculatePrice(size, dsize);
            
            if(type=="assiette" && size=="moyen" && dsize=="normal" && coffee=="yes")
            {
                Console.Write(" avec café offert!");
            } else {
                total+=1;
            }
            return total;
        }
    }
}