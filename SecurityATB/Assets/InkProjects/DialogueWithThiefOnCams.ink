You: Hello
Client: Hello
 *You: Why are you steal this?->nextLine

 *You: You are under arrest->DONE
 
 ===nextLine===
 {
 -RANDOM (1, 2) == 1:
 Client: ...
 -RANDOM (1, 2) == 2: 
 Client: I`m so sorry! I just must bring this to my sick mom!
 - else:
 Client: I want eat
 }
*You: I need to call the police->DONE
*You: OK, you can go->DONE
