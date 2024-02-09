-> main

=== main ===
You: Hello, it is security, can you show me your backpack/pockets?
{
- RANDOM(1, 5) > 1:
   -> client_here
- else: 
   -> client_run
}
-> DONE

=== client_here ===
Client: Hello, yes of course.
You: I hope this product here is a coincidence?
{
- RANDOM(1, 5) > 1:
   Client: ....
- else:
   Client: I don't understand how it got here! This is definitely a coincidence!
}
* You: Well, you will decide this with the police
{
- RANDOM(1, 5) > 1:
   Client: No!!! Please!!! It's just my holey head, I didn't do anything wrong!
   -> pre_end
- else: -> client_run
}
* You: Okay, you can go, just don't forget anything in your pockets/backpack anymore-> DONE
=== pre_end ===
* You: I still have to call the police ->DONE
* You: Okay, you can go, just don't forget anything in your pockets/backpack anymore->DONE
=== client_run ===
"Client run away"-> DONE