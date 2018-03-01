	[section _code, Code]
	nop
	push 0xFF
	push *N
	pop
	pop

	[section _data, Data]
N:	dd 0x10 		; N-th number of fibonacci
i:	dd 0x0 		 
a:	dd 0x0		
b:	dd 0x1
arr: times 0x10 dd 0xFF