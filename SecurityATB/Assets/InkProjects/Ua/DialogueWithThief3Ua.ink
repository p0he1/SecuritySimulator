-> main

=== main ===
Ви: Доброго дня, я з охорони. Чи можу я оглянути ваш рюкзак/кишені?
{
- RANDOM(1, 5) > 1:
   -> client_here
- else: 
   -> client_run
}
-> DONE

=== client_here ===
Клієнт: Доброго дня, звичайно можете.
Ви: Я припускаю, що цей предмет тут просто збіг?
{
- RANDOM(1, 5) > 1:
   Клієнт: ....
- else:
   Клієнт: Я здивований, як воно туди потрапило! Це якесь непорозуміння!
}
* Ви: Нам потрібно буде звернутися до поліції.
{
- RANDOM(1, 5) > 1:
   Клієнт: Я не хотів зробити нічого поганого! Вибачте мені, будь ласка!
   -> pre_end
- else: -> client_run
}
* Ви: Зрозуміло, ви можете піти, але не залишайте нічого більше в рюкзаку.-> DONE
=== pre_end ===
* Ви: Я все ще маю зконтактувати з поліцією->DONE
* Ви: Зрозуміло, ви можете піти, але не залишайте нічого більше в рюкзаку.->DONE
=== client_run ===
"Client run away"-> DONE