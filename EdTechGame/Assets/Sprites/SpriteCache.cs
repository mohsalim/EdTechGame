﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Sprites
{
    public class SpriteCache
    {
        /// <summary>
        /// Cache for requested resources.
        /// </summary>
        private static Dictionary<string, Sprite> cache = new Dictionary<string, Sprite>();

        /// <summary>
        /// Used to load all sprite resources.
        /// </summary>
        private static Sprite[] loadedSprites = null;

        /// <summary>
        /// Loads sprites efficiently.
        /// For some reason, loading explicit file name using Resources.Load<Sprite>("ASSET FILE NAME") fails.
        /// This method loads all sprites and filters by name property. Caches as a new resource is requested.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Sprite GetSprite(string name)
        {
            // If there are no loaded sprites, then load them.
            if (loadedSprites == null)
            {
                Debug.Log("Sprites not loaded. Loading now.");
                loadedSprites = Resources.LoadAll<Sprite>("");
                Debug.Log("Number of sprites loaded: " + loadedSprites.Length);
            }

            Debug.Log("Loading sprite: " + name);
            
            // Check to see if sprite exists in cache.
            if (cache.ContainsKey(name))
            {
                Debug.Log("Found sprite in cache.");
                return cache[name];
            }

            // Find sprite in loaded resources.
            Sprite sprite = loadedSprites.FirstOrDefault(s => s.name == name);

            // If the sprite dose not exist, then log the error.
            if (sprite == null)
            {
                Debug.LogError("Sprite does not exist in resources.");
                return null;
            }

            // Add sprite to cache.
            Debug.Log("Adding sprite to cache.");
            cache[name] = sprite;

            // Return sprite.
            return sprite;
        }
    }
}
