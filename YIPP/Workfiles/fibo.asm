	times 0x10 nop
	[section _code, Code]
entry:
	jmp fibo

; FUNCTIONS
func_dec_i:
	push *i
	dec
	save *i 	; i--

	ret

fibo:
	read
	save *i 	; i = N
loop:
	push *a
	push *b

	; analog of for (i = N; i > 0; i--)
	
	add 		; top = a + b
	
	push *a
	save *b 	; b = a

	save *a 	; a = top

	call func_dec_i, *i

	push *i
	jnz loop
result:
	push *a
	print   	; print a
	
	[section _data, Data]
N:	dd 0x10 		; N-th number of fibonacci
i:	dd 0x0 		 
a:	dd 0x0		
b:	dd 0x1
	