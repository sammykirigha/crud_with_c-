using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
               
            };

        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await this.context.SuperHeroes.ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await this.context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Hero not found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero hero)
        {
            this.context.Add(hero);
            await this.context.SaveChangesAsync();
            return Ok(this.context.SuperHeroes);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbhero = await this.context.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null) return BadRequest("Hero not found");

            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(this.context.SuperHeroes.ToArrayAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {

            var dbhero = await this.context.SuperHeroes.FindAsync(id);
            if (dbhero == null) return BadRequest("Hero not found");

            this.context.SuperHeroes.Remove(dbhero);
            await this.context.SaveChangesAsync();

            return Ok("Hero Deleted successfully");
        }
    }
}
