using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BarbarianMeetsCoding.IntroductionToMEF
{
    class Program
    {

        /* Iterate over Example -> Evil Wizard has a spell book with spells */

        static void Main(string[] args)
        {
            // We use a catalog to discover all parts within the executing assembly
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            // We use a container to compose parts based on the specified contracts. 
            // The container, in turn, uses a catalog to find out the available parts.
            CompositionContainer container = new CompositionContainer(catalog);

            EvilWizard evilWizard = new EvilWizard();
            container.ComposeParts(evilWizard);

            System.Console.WriteLine("The Evil Wizard Wears a " + evilWizard.Hat + " and a " + evilWizard.Robe);
            foreach (var spell in evilWizard.Spells)
            {
                spell.Cast();
            }

        }
    }

    public class EvilWizard
    {
        public string Name { get; set; }

        [Import]
        public Hat Hat { get; set; }
        [Import]
        public Robe Robe { get; set; }
        
        [ImportMany]
        public IEnumerable<ISpell> Spells { get; set; }

    }

    [Export]
    public class Hat
    {
        public string Name { get { return "Shiny Hat"; } }
        public override string ToString()
        {
            return Name;
        }
    }

    [Export]
    public class Robe
    {
        public string Name { get { return "Cloak of Heavenly Wisdom"; } }
        public override string ToString()
        {
            return Name;
        }
    }

    [InheritedExport]
    public interface ISpell
    {
        void Cast();
    }

    public class LightningSpell : ISpell
    {
        public void Cast()
        {
            System.Console.WriteLine("Evil Wizard casts lightning spell!");
        }
    }

    public class SleepSpell : ISpell
    {
        public void Cast()
        {
            System.Console.WriteLine("Evil Wizard casts sleep spZzZzzZzZzZZzZ");
        }
    }
}
