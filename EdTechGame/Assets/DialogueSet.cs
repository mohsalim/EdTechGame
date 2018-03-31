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
                                "I think we have to print 'hello word' followed by 'goodbye world', but I'm not sure how to do that!"
                            },
                successMessage = "Nice bro! I think that was right!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }

        /// <summary>
        /// Dialogue for NumberVariableProblem.cs.
        /// </summary>
        /// <returns></returns>
        public static Dialogue NumberVariableDialogue()
        {
            return new Dialogue
            {
                name = NpcNames.BROSEPH,
                sentences = new string[]
                            {
                                "The assignment says that we to change the variable 'number' to 8.",
                                "But bro, it also says we got to do it before printing the variable."
                            },
                successMessage = "That seems right. You're so smart bro!",
                sprite = NpcNames.BROSEPH_SPRITE_HAPPY
            };
        }
    }
}
