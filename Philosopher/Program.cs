using Philosopher;
Console.OutputEncoding = System.Text.Encoding.UTF8;
Logger logger = new Logger();
logger.Start();
List<Philosoph> philosophers = new List<Philosoph>();
TableForPhilosophers tableForPhilosophers = new TableForPhilosophers(logger);
for (int i = 1; i < 5; i++)
{
    philosophers.Add(new Philosoph($"Fill{i}", logger, tableForPhilosophers));
}
foreach (var philosoph in philosophers)
{
    philosoph.PhilosopherLive();
}

Console.ReadLine();
logger.Stop();
logger.Dispose();
foreach (var philosoph in philosophers)
{
    philosoph.PhilsopherDead();
}
