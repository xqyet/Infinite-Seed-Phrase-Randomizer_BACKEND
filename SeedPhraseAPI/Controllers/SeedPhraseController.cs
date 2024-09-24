using Microsoft.AspNetCore.Mvc;
using NBitcoin;
using System.Collections.Generic;
using System.Linq;

namespace SeedPhraseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedPhraseController : ControllerBase
    {
        // Updated seed phrase with 11 known words, last word unknown
        private static string[] knownWords = { "world", "subway", "wagon", "vacant", "lunar", "inmate", "vendor", "just", "equal", "canal", "swing" };

        // BIP-39 word list for the last word
        private static string[] bip39Words = Wordlist.English.GetWords().ToArray();

        // GET api/SeedPhrase/All
        [HttpGet("All")]
        public ActionResult<IEnumerable<string>> GetAllSeedPhrases()
        {
            // Generate all possible seed phrases with the 12th word coming from BIP-39 word list
            var seedPhrases = bip39Words.Select(word => string.Join(" ", knownWords.Concat(new[] { word })));
            return Ok(seedPhrases);
        }
    }
}
