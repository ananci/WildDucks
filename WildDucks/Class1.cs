using StardewModdingAPI;
using StardewValley;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildDucks
{
    public class WildDucks : Mod
    {
        String currentSeason;

        // Ducks Prefer Spring and Fall
        String[] preferredSeasons = new String[2] { "spring", "fall" };

        // Base probability of duck spawning
        double baseProb = 0.1;

        // Season and weather mods
        double seasonProb = 0.25;
        double weatherProb = 0.5;

        // Set up for some randomization here and some stored values
        private readonly Random random = new Random();

        // Will ducks spawn
        bool duckSpawn;

        public override void Entry(IModHelper helper)
        {
            base.Entry(helper);
            StardewModdingAPI.Events.TimeEvents.DayOfMonthChanged += TimeEvents_ShouldWildDucksSpawn;
            StardewModdingAPI.Events.LocationEvents.CurrentLocationChanged += LocationEvents_SpawnWildDucks;
            Log.Verbose("WildDucks by Ananci => Initialized");
            Log.Verbose("Season: " + StardewValley.Game1.currentSeason);

        }

        private void LocationEvents_SpawnWildDucks(object sender, StardewModdingAPI.Events.EventArgsCurrentLocationChanged e)
        {
            StardewValley.CoopDweller wduck = new StardewValley.CoopDweller("duck", "wild");
            wduck.setRandomPosition();
            Log.Verbose("Location Change Detected: " + e.NewLocation);
            if (e.NewLocation is StardewValley.Locations.Forest)
            {
                Log.Verbose("Ducks will spawn in the Forest: " + duckSpawn);
                if (duckSpawn)
                {
                    // Play the flock sound
                    Quackers();


                    // Spawn a duck
                    /*
                    new StardewValley.CoopDweller("duck", "wild").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild2").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild3").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild4").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild5").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild6").setRandomPosition();
                    new StardewValley.CoopDweller("duck", "wild7").setRandomPosition();
                    */
                    // Spawn a duck
                    /*
                    new StardewValley.CoopDweller("Duck", "wild").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild2").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild3").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild4").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild5").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild6").setRandomPosition();
                    new StardewValley.CoopDweller("Duck", "wild7").setRandomPosition();
                    */
                }
            }
        }

        private void Quackers()
        {
            StardewValley.Game1.playSound("Duck");
            Task.Delay(1);
            StardewValley.Game1.playSound("Duck");
            Task.Delay(9);
            StardewValley.Game1.playSound("Duck");
            Task.Delay(75);
            StardewValley.Game1.playSound("Duck");
        }

        private void TimeEvents_ShouldWildDucksSpawn(object sender, EventArgs e)
        {
            //This will check if wild ducks should spawn every new game day
            currentSeason = StardewValley.Game1.currentSeason;

            double probSpawn = baseProb;
            // Ducks should prefer to spawn on the following conditions:
            // 1. Spring
            // 2. Fall
            // 3. Raining
            // Ducks should have a small chance of spawning at other times.
            if (preferredSeasons.Contains(currentSeason))
            {
                Log.Verbose(currentSeason);

                probSpawn = probSpawn + seasonProb;
            }

            if (StardewValley.Game1.isRaining)
            {
                probSpawn = probSpawn + weatherProb;
            }
            Log.Verbose("Season: " + currentSeason);
            Log.Verbose("Prob of Ducks: " + probSpawn);
            double rand = random.NextDouble();
            duckSpawn = (rand < probSpawn);

            Log.Verbose("Rolled: " + rand);
            Log.Verbose("Ducks will spawn: " + duckSpawn);
        }

    }
}
