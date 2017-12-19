using System;
using System.Net.Http;
using System.Threading.Tasks;
using LitJson;

namespace Pokemon
{
    public class Pokedex
    {
        private enum API_STATS 
        {
            SPEED,
            SPEC_DEF,
            SPEC_ATK,
            DEF,
            ATK,
            HP
        }
        
        private const string WIKI_URL = "http://pokeapi.co/api/v2/pokemon/";
        private const string API_404 = "{\"detail\":\"Not found.\"}";

        private static Pokemon.Poketype StrToType(string s)
        {
            switch (s)
            {
                case "POISIN":
                    return Pokemon.Poketype.FIRE;
                case "FIRE":
                    return Pokemon.Poketype.FIRE;
                case "WATER":
                    return Pokemon.Poketype.WATER;
                case "GRASS":
                    return Pokemon.Poketype.GRASS;
                case "ELECTRIC":
                    return Pokemon.Poketype.ELECTRIC;
                default:
                    return Pokemon.Poketype.OTHER;
            }
        }
        
        public async Task GetPokemon(string name, Pokemon pk)
        {
            HttpClient hc = new HttpClient();
            Console.WriteLine("Querying {0}'s info...", name);
            HttpResponseMessage response = await hc.GetAsync(WIKI_URL + name.ToLower());
            string responseString = await response.Content.ReadAsStringAsync();

            if (responseString == API_404)
            {
                Console.WriteLine("Woah, looks like {0} doesn't exist !", name);
                return;
            }

            /* Parse the API's data */
            
            JsonData api_data = JsonMapper.ToObject(responseString);
            
            /* Rename the pokemon (name defaults to "not found". */
            pk.Name = (api_data["name"].ToString());

            string hp = api_data["stats"][(int) API_STATS.HP]["base_stat"].ToString();
            pk.Life = int.Parse(hp);

            string speed = api_data["stats"][(int) API_STATS.SPEED]["base_stat"].ToString();
            pk.Speed = int.Parse(speed);

            string attack = api_data["stats"][(int) API_STATS.ATK]["base_stat"].ToString();
            pk.Attack = int.Parse(attack);

            string defense = api_data["stats"][(int) API_STATS.DEF]["base_stat"].ToString();
            pk.Life = int.Parse(defense);
            
            
            /* HARDCODED: Get the pokemon's first type. */
            string type = api_data["types"][0]["type"]["name"].ToString().ToUpper();
            pk.Poketype_ = StrToType(type);
        }
    }
}