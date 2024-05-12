-> main

=== main ===
You: Hello, it is security, may I inspect your backpack and pockets?
{
- RANDOM(1, 5) > 1:
   -> client_here
- else: 
   -> client_run
}
-> DONE

=== client_here ===
Client: Hello, certainly.
You: I trust this item here is merely a coincidence?
{
- RANDOM(1, 5) > 1:
   Client: ....
- else:
   Client: I am baffled as to how it ended up here! It is purely coincidental!
}
* You: You will need to discuss this with the police.
{
- RANDOM(1, 5) > 1:
   Client: No!!! Sorry, I'm just a bit forgetful!
   -> pre_end
- else: -> client_run
}
* You: OK, you may go, but please remember not to leave anything in your pockets or backpack.-> DONE
=== pre_end ===
* You: I still must inform the police.->DONE
* You: OK, you may go, but please remember not to leave anything in your pockets or backpack.->DONE
=== client_run ===
"Client run away"-> DONE