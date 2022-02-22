using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private List<Superhero> heroes = new List<Superhero>
        {
            new Superhero
            {
                Id = 0,
                Name = "Spiderman",
                FirstName = "Peter",
                LastName = "Parker",
                Place = "New York City"
            },
            new Superhero
            {
                Id = 1,
                Name = "Ironman",
                FirstName = "Tony",
                LastName = "Stark",
                Place = "Long Island"
            }
        };
        private readonly DataContext dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Superhero>>> Get()
        {
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Superhero>> Get(int id)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<Superhero>>> AddHero(Superhero hero)
        {
            dataContext.SuperHeroes.Add(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Superhero>>> UpdateHero(Superhero request)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero not found.");

            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Superhero>> Delete(int id)
        {
            var hero = await dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found.");

            dataContext.Remove(hero);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.SuperHeroes.ToListAsync());
        }
    }
}
