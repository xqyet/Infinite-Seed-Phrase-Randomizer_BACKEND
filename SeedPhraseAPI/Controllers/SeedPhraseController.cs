using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedPhraseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedPhraseController : ControllerBase
    {
        // BIP-39 word list for generating random seed phrases
        private static string[] bip39Words = Wordlist.English.GetWords().ToArray();

        // GET api/SeedPhrase/Random/{count}
        [HttpGet("Random/{count?}")]
        public ActionResult<IEnumerable<string>> GetRandomSeedPhrases(int count = 2000) // Default count set to 400
        {
            var seedPhrases = new List<string>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var selectedWords = new List<string>();
                var letterCount = new Dictionary<char, int>();

                while (selectedWords.Count < 12)
                {
                    var word = bip39Words[random.Next(bip39Words.Length)];
                    char firstLetter = word[0];

                    if (!letterCount.ContainsKey(firstLetter))
                    {
                        letterCount[firstLetter] = 0;
                    }

                    // Allow no more than 3 words starting with the same letter
                    if (letterCount[firstLetter] < 3)
                    {
                        letterCount[firstLetter]++;
                        selectedWords.Add(word);
                    }
                }

                var seedPhrase = string.Join(" ", selectedWords);
                seedPhrases.Add(seedPhrase);
            }

            return Ok(seedPhrases);
        }
    }
}
