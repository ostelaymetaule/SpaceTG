using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramGame.Lib
{
    public class GameLogic
    {
        private Dictionary<string, Card> AllCards = new Dictionary<string, Card>() { };
        private Dictionary<string, Transition> AllTransitions = new Dictionary<string, Transition>() { };
        private Dictionary<string, GameState> Games { get; set; }


        #region init

        private void InitCards()
        {
            AllCards.Add(
                "Start", new Card()
                {
                    Id = "Start",
                    Text = "You are started from the Moon base into the void on the long journey to Mars. Choose your ship class.",
                    Transitions = new List<string>()
                    {
                        "Schooner",
                        "Brig",
                        "Frigate"
                    }
                });
            AllCards.Add(
                "Solar wind", new Card()
                {
                    Id = "Solar wind",
                    Text = "Heavy stream of fast moving ions and plasma will intercept your course in the next 8 sol. Your actions?!",
                    Transitions = new List<string>()
                    {
                        "Correct the orbit using thrust",
                        "Drop some ballast to create impulse",
                        "Rise shields"
                    }
                });
            AllCards.Add(
                "Gravity well", new Card()
                {
                    Id = "Gravity well",
                    Text = "Your course will pass near the gravity well of the planet",
                    Transitions = new List<string>()
                    {
                        "Slingshot",
                        "Keep distance"
                    }
                });
            AllCards.Add(
                "Pirates", new Card()
                {
                    Id = "Pirates",
                    Text = "You are beeing followed by an unknown ship. It should be pirates!",
                    Transitions = new List<string>()
                    {
                        "Give them cargo",
                        "Increase impulse",
                        "Give them broadside"
                    }
                });
            AllCards.Add(
                "Bored", new Card()
                {
                    Id = "Bored",
                    Text = "Travel takes a long time and yur crew is bored.",
                    Transitions = new List<string>()
                    {
                        "Give them vine!",
                        "Increase rations",
                        "Do manevouring excercises"
                    }
                });

        }

        private void InitTransitions()
        {
            AllTransitions.Add("Slingshot", new Transition()
            {
                Id = "Slingshot",
                Text = "Use planets's gravity to perform a slingshot maneuver.",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -1, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Coeffitient, ChangeValue = 1.2, WhatChanges = Asset.AssetNames.Speed}
                },
                PossibleCardNames = new List<string>() { "Gravity well", "Bored" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 88, WhatToTest = Asset.AssetNames.Speed },
                    new Condition() { HowToTest = Condition.ConditionVariant.LessThen, TestValue = 1000, WhatToTest = Asset.AssetNames.Speed }
                }
            });
            AllTransitions.Add("Keep distance", new Transition()
            {
                Id = "Keep distance",
                Text = "Keep away from the planets gravity well, to not to crash. It might be slow, but safe.",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -2, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 10, WhatChanges = Asset.AssetNames.Speed}
                },
                PossibleCardNames = new List<string>() { "Bored" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.LessThen, TestValue = 200, WhatToTest = Asset.AssetNames.Speed }
                }
            });
            AllTransitions.Add("Give them vine!", new Transition()
            {
                Id = "Give them vine!",
                Text = "You are poping some of your personal reserve but the crew apreciates that!",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -1, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 1, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Coeffitient, ChangeValue = 1.1, WhatChanges = Asset.AssetNames.Crew},

                },
                PossibleCardNames = new List<string>() { "Bored", "Pirates", "Solar wind" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 1, WhatToTest = Asset.AssetNames.Cargo },
                }
            });
            AllTransitions.Add("Increase rations", new Transition()
            {
                Id = "Increase rations",
                Text = "Tasty food is the best entertainment",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -2, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 1, WhatChanges = Asset.AssetNames.Moral}
                },
                PossibleCardNames = new List<string>() { "Bored", "Pirates", "Solar wind" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 2, WhatToTest = Asset.AssetNames.Cargo },
                }
            });
            AllTransitions.Add("Do manevouring excercises", new Transition()
            {
                Id = "Do manevouring excercises",
                Text = "The crew must be ready for everything and you should use the free time to train for it",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -2, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -1, WhatChanges = Asset.AssetNames.Moral}
                },
                PossibleCardNames = new List<string>() { "Bored", "Solar wind", "Gravity well" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 1, WhatToTest = Asset.AssetNames.Moral },
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 2, WhatToTest = Asset.AssetNames.Fuel },
                }
            });
            AllTransitions.Add("Give them cargo", new Transition()
            {
                Id = "Give them cargo",
                Text = "It's only cargo",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -2, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -1, WhatChanges = Asset.AssetNames.Moral}
                },
                PossibleCardNames = new List<string>() { "Bored", "Solar wind", "Gravity well" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 2, WhatToTest = Asset.AssetNames.Cargo },
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 1, WhatToTest = Asset.AssetNames.Moral },
                }
            });
            AllTransitions.Add("Increase impulse", new Transition()
            {
                Id = "Increase impulse",
                Text = "Try to run from them",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -2, WhatChanges = Asset.AssetNames.Fuel},
                },
                PossibleCardNames = new List<string>() { "Bored", "Pirates" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 2, WhatToTest = Asset.AssetNames.Fuel },
                }
            });
            AllTransitions.Add("Give them broadside", new Transition()
            {
                Id = "Give them broadside",
                Text = "Come and get some",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 1, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -1, WhatChanges = Asset.AssetNames.Crew},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well", "Solar wind" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 1, WhatToTest = Asset.AssetNames.Crew },
                }
            });
            AllTransitions.Add("Give them broadside", new Transition()
            {
                Id = "Give them broadside",
                Text = "Come and get some",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 1, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Coeffitient, ChangeValue = 0.8, WhatChanges = Asset.AssetNames.Crew},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well", "Solar wind" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 1, WhatToTest = Asset.AssetNames.Crew },
                }
            });
            AllTransitions.Add("Correct the orbit using thrust", new Transition()
            {
                Id = "Correct the orbit using thrust",
                Text = "Increase impulse to fly at the safe distance from the ion stream",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -10, WhatChanges = Asset.AssetNames.Fuel},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 10, WhatToTest = Asset.AssetNames.Fuel },
                }
            });
            AllTransitions.Add("Drop some ballast to create impulse", new Transition()
            {
                Id = "Drop some ballast to create impulse",
                Text = "We do not need so much potatos anyway",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Coeffitient, ChangeValue = 0.7, WhatChanges = Asset.AssetNames.Cargo},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 10, WhatToTest = Asset.AssetNames.Cargo },
                }
            });
            AllTransitions.Add("Rise shields", new Transition()
            {
                Id = "Rise shields",
                Text = "We are using some amount of cargo to construct protective shelter to reduce the damage",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -5, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = -5, WhatChanges = Asset.AssetNames.Crew},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well", "pirates" },
                Conditions = new List<Condition>()
                {
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 5, WhatToTest = Asset.AssetNames.Cargo },
                    new Condition() { HowToTest = Condition.ConditionVariant.GreaterThenOrEqual, TestValue = 5, WhatToTest = Asset.AssetNames.Crew },

                }
            });
            AllTransitions.Add("Schooner", new Transition()
            {
                Id = "Schooner",
                Text = "Fast and nimble, this vessel is not suited to transport a large amount of cargo",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 20, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 5, WhatChanges = Asset.AssetNames.Crew},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 20, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 5, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 3, WhatChanges = Asset.AssetNames.Speed},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 20, WhatChanges = Asset.AssetNames.Water},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 0, WhatChanges = Asset.AssetNames.RediationLevel},

                },
                PossibleCardNames = new List<string>() { "Bored", "Gravity well" },
                Conditions = new List<Condition>()
                {
                }
            });
            AllTransitions.Add("Brig", new Transition()
            {
                Id = "Brig",
                Text = "Median in every well, this ship is a good fit for anyone",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 50, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 10, WhatChanges = Asset.AssetNames.Crew},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 50, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 2, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 2, WhatChanges = Asset.AssetNames.Speed},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 50, WhatChanges = Asset.AssetNames.Water},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 0, WhatChanges = Asset.AssetNames.RediationLevel},

                },
                PossibleCardNames = new List<string>() { "Bored", "Solar wind" },
                Conditions = new List<Condition>()
                {
                }
            });
            AllTransitions.Add("Frigate", new Transition()
            {
                Id = "Frigate",
                Text = "Large and slow, this vessel is well equipped for a long travel.",
                Effects = new List<Effects>()
                {
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 150, WhatChanges = Asset.AssetNames.Cargo},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 15, WhatChanges = Asset.AssetNames.Crew},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 100, WhatChanges = Asset.AssetNames.Fuel},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 2, WhatChanges = Asset.AssetNames.Moral},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 1, WhatChanges = Asset.AssetNames.Speed},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 100, WhatChanges = Asset.AssetNames.Water},
                    new Effects(){HowChanges = Effects.EffectVariant.Offset, ChangeValue = 0, WhatChanges = Asset.AssetNames.RediationLevel},

                },
                PossibleCardNames = new List<string>() { "Bored", "Pirates" },
                Conditions = new List<Condition>()
                {
                }
            });
        }

        #endregion init

        private GameState InitSimpleGame(string userId = "TestUser")
        {
            var simpleState = new GameState();
            simpleState.Assets.Add(Asset.AssetNames.Crew, new Asset() { Name = Asset.AssetNames.Crew, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.Fuel, new Asset() { Name = Asset.AssetNames.Fuel, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.RediationLevel, new Asset() { Name = Asset.AssetNames.RediationLevel, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.Water, new Asset() { Name = Asset.AssetNames.Water, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.Cargo, new Asset() { Name = Asset.AssetNames.Water, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.Moral, new Asset() { Name = Asset.AssetNames.Water, Value = 0 });
            simpleState.Assets.Add(Asset.AssetNames.Speed, new Asset() { Name = Asset.AssetNames.Water, Value = 0 });

            simpleState.UserId = userId;
            simpleState.PlayableCards = new List<Card>()
            {
                AllCards["Start"]
            };

            return simpleState;
        }

        public Card StartNewGame(string userId = "TestUser")
        {
            var gamestate = InitSimpleGame(userId);
            Games[userId] = gamestate;
            return PickCard(gamestate.PlayableCards);
        }

        public void MakeTransition(string userId, string choice)
        {
            var currentState = Games[userId];

            var transiton = AllTransitions[choice];
            //todo: change the state accordingly to the transitions rules
        }

        public Card PickCard(List<Card> cards)
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            int randomCardIndex = r.Next(cards.Count);

            var pickedCard = cards[randomCardIndex];
            //todo: reduce the possible transitions for this card accordingly to the conditions and state
            return pickedCard;
        }

    }
}
