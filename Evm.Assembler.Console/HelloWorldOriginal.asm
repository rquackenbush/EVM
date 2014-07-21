#entrance point
	push_1, 2
	push_1, 7
	add_u1
	write_u1
	call, :Everything
	return

#function
:Everything
	push_1, 42
	write_u1
	return


