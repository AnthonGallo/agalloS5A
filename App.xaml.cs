namespace agalloS5A
{
    public partial class App : Application
    {
        public static Repository.PersonRepository personRepo {  get; set; }
        public App(Repository.PersonRepository personRepository)
        {
            personRepo = personRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Views.vHome());
        }
    }
}