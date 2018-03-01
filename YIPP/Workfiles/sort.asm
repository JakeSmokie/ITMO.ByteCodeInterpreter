entry:
	jmp main

; FUNCTIONS
func_input_amount:
	read
	save *n

	push 0x2
	push *n
	cmp2

	jlz exit

	push 0x20
	push *n
	cmp2

	jgz exit
	ret

func_input_elem:
	ldarg 0x0 
	save *addelemindex

	read
	save [arr], *addelemindex

	ret

func_fill_arr:
	push 0x0
	save *i 			; i = 0
.______loop:
	; the same loop as
	; for (i = 0; i < n; i++)

	random 0x0, 0xFF
	save [arr], *i 		; arr[i] = random()

	push [arr], *i
	print				; print arr[i]

	push *i
	inc
	save *i 			; i++

	push *i
	push *n
	cmp2

	jnz .______loop			; if (i != n) then continue else break
	ret

func_build_tree:
	push 0x0 			; i = 0
	save *i
.loop:
	call func_input_elem, *i
	call func_insert, *i; insert(i)

	push *i
	inc
	save *i 			; i++

	push *i
	push *n
	cmp2

	jnz .loop			; if (i != n) then continue else break
	ret

func_print_tree:
	;call func_debug

	push 0x0
	save *cur  	; cur = 0
.loop: 						; while(1)
	..left:
		push *cur
		jlz ..visit 		; if cur element == NULL goto visit

		push *cur 			; add to stack current element

		push [left], *cur
		save *cur    		; (current element) = (left child)

		jmp ..left
	..visit:
		empty
		jgz .end 			; if (is stack empty) then break

		save *cur 			; pop value from stack
		
		push [arr], *cur
		print  				; print arr[cur]

	..right:
		push [right], *cur
		save *cur 			; (current element) = (right child)

		jmp .loop  			; continue
.end:	
	ret

func_debug:
	times 0x3 push 0x7FFFFFFF
	times 0x3 print				; just some debug info

	ret

func_insert:
	ldarg 0x0

	save *addelemindex  ; addelemindex = first argument
	push *addelemindex	; if addelemindex = 0 -> goto addzero else goto setheadindex

	jez .addzero
	jmp .setheadindex
.addzero:
	push 0x1
	save [exist], *addelemindex	; exist[addelemindex] = 1

	jmp .end
.setheadindex:
	push 0x0
	save *cur				; cur = 0
.loop: 						; while (current element)
	push [exist], *cur
	jez .end				; statement check

	push [arr], *cur
	push [arr], *addelemindex

	cmp2
	jlz ..less  			

	..greater:
		push [right], *cur
		jlz ...child_doesnt_exist
		jmp ...child_exist

		...child_exist:				; we go next right child
			push [right], *cur
			save *cur
			jmp .loop
		...child_doesnt_exist: 		; we create new element of tree
			push *addelemindex
			save [right], *cur

			push 0x1
			save [exist], *addelemindex

			jmp .end
	..less: 	
		push [left], *cur
		jlz ...child_doesnt_exist
		jmp ...child_exist

		...child_exist: 			; the same as right
			push [left], *cur
			save *cur
			jmp .loop
		...child_doesnt_exist:
			push *addelemindex
			save [left], *cur

			push 0x1
			save [exist], *addelemindex

			jmp .end
.end:
	ret

main:
	call func_input_amount
	;call func_fill_arr
	call func_build_tree
	call func_print_tree
exit:
	nop
	
	[section _data, Data]
i:		dd 0x0 		; counter
n:		dd 0x20 	; amount of numbers

cur:			dd 0x0			; index of element to operate with
addelemindex: 	dd 0xFFFFFFFF 	; index of element to add

arr:	times 0x20 dd 0x0 			; values
left:	times 0x20 dd 0xFFFFFFFF	; pointer for left child 
right:	times 0x20 dd 0xFFFFFFFF	; pointer for right child
exist:	times 0x20 dd 0x0 			; is binary tree cell exist (is not NULL ?)

