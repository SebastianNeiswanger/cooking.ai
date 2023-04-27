# cooking.ai
This repo is dedicated to the creation of a diner dash like game where the character is controlled by a trained ai. This game will be made in Unity and use the ml-agents add-on.

## The Goal
Have AI play a diner dash game where it makes orders as fast as it can in a changing kitchen. The orders are recieved from panman (the customer). This was decided to be done making use of a curriculum system.

## Where we got
By the end of the time on our project we got to the point of training. Most of our time spent was used on setting up the game, and making sure that part works before the agent was given free reign. Spatman (our agent) is able to move in 8 directions (N, S, E, W, and their combinations) as well as not move, and interact. 

## Our Agent
This is our ending layout of rewards and inputs:
- 308 inputs
  - serialized kitchen layout
  - held item
  - location
  - a few others items
- 3 Discrete Branches
  - 3 Values (horizontal movement)
  - 3 Values (vertical movement)
  - 2 Values (Interact)
- Training rewards
  - Surviving (bad)
  - Moving (good)
  - Getting buns (good)
  - Getting plates (good)
  - Getting beef (good)
  - Getting cooked beef (good)
  - Get materials that are in an order (good)
  - Start cooking or cutting (good)
  - Deliver a correct order (good)
  - Deliver an incorrect order (good)
  - Putting something on a counter (good)
  - Putting Something right back down (bad)
  - Getting a bun, cooked beef, and plate (good)
  - Getting a full burger (good)
  - Putting materials back in a box (bad)
- Curriculum Lessons
  - Get beef – success
  - Cook beef – failure
  - Get cooked beef, buns, and plate
  - Get a full burger
  - Serve Panman a full burger
  - Serve Panman based on his order
  - Serve Panman multiple orders succesfully

Here is a funny interaction we ended up encountering. We are not sure how it flies, nor why it learned to do it, but I think is funny none the less.
![Spatman 2: Spatman in Space](https://user-images.githubusercontent.com/31674857/234959943-c8dfc28d-d9b5-4bea-93ef-98359c828fa1.mp4)
