-> main

=== main ===
You: Hello, I am from security. May I examine your backpack and pockets, please?
{
- RANDOM(1, 5) > 1:
   -> client_here
- else: 
   -> client_run
}
-> DONE

=== client_here ===
Client: Greetings, of course.
You: I assume this item here is just a coincidence?
{
- RANDOM(1, 5) > 1:
   Client: ....
- else:
   Client: I am puzzled as to how it got there! This is some misunderstanding!
}
* You: You will need to address this with the police.
{
- RANDOM(1, 5) > 1:
   Client: I didn't mean to do anything wrong! Please forgive me!
   -> pre_end
- else: -> client_run
}
* You: Understood, you are free to leave, but please be mindful not to leave anything behind in your pockets or backpack.-> DONE
=== pre_end ===
* I still have to contact the police.->DONE
* You: Understood, you are free to leave, but please be mindful not to leave anything behind in your pockets or backpack.->DONE
=== client_run ===
"Client run away"-> DONE