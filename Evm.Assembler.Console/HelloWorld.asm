#entrance point
	push_1, 2
	push_1, 7
	add_u1
	write_u1
	call, :AnswerToEverything
	return

#function
:AnswerToEverything
	push_1, 42
	write_u1
	call, :AnotherFunction
	return

#function 
:AnotherFunction
	push_1, 234
	write_u1
	return

