using Assets.Tutorials;

namespace Assets
{
    public class DialogueSet
    {
        /// <summary>
        /// Dialogue for HelloWorldProblem.cs.
        /// </summary>
        /// <returns></returns>
        public static Dialogue HelloWorldDialgoue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Bro, did you finish the programming homework?",
                    "I can't figure it out! Can you please help me solve it?",
                    "Come on bro! I'll pay you back big time time.",
                    "Brooooooooooooooooooooooooooooooooooooooooooooo, please?!",
                    "I think we have to print 'hello word' followed by 'goodbye world', but I'm not sure how to do that!",
                    "The text inside quotes is referred to as strings. We can use single quotes or double quotes. Take your pick."
                },
                successMessage = "Nice bro! I think that was right!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        /// <summary>
        /// Dialogue for IntegeProblem.cs.
        /// </summary>
        /// <returns></returns>
        public static Dialogue IntegerDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Now we're looking at integers. Integers are whole numbers like 1, 2, -5, etc.",
                    "The assignment says that we to change the variable 'number' to 8.",
                    "But bro, it also says we got to do it before printing the variable."
                },
                successMessage = "That seems right. You're so smart bro!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        /// <summary>
        /// Dialogue for FloatingPointProblem.cs.
        /// </summary>
        /// <returns></returns>
        public static Dialogue FloatingPointDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                {
                    "Next up is floating points. These are numbers with decimals like 1.2, 4.0, -0.2, etc.",
                    "You can write floating points as is or you can convert integers to floats using functions.",
                    "Functions can be confusing at first bro, but all they do is take in variables and return a new variable.",
                    $"You can see from {NpcNames.PROFESSOR}'s example how to do both. Prof wants us to print out a variable with 8.2."
                },
                successMessage = "You're catching on. You deserve a brophy!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }
    }
}
