	times 0x10 nop
	[section _code, Code]
entry:
	jmp fillarr 	; Skip functionss

; FUNC INSERT START
func_insert:
	.start:
		push *amount
		inc
		save *amount	; amount++

		push *amount	; if amount = 0 -> goto addzero else goto setheadindex

		jez .addzero
		jmp .setheadindex

	.addzero:
		push 0x1
		save [exist], *amount	; exist[amount] = 1

		jmp .end
	.setheadindex:
		push 0x0
		save *cur				; cur = 0

	.loop: 						; while (current element)
		push [exist], *cur
		jez .end				; statement check

		push [arr], *cur
		push [arr], *amount

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
				push *amount
				save [right], *cur

				push 0x1
				save [exist], *amount

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
				push *amount
				save [left], *cur

				push 0x1
				save [exist], *amount

				jmp .end
	.end:
		ret

; FUNC INSERT END

; =============================
; Fill Array
; =============================
fillarr:
	push 0x0
	save *i 			; i = 0
.loop:
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

	jnz .loop			; if (i != n) then continue else break

; =============================
; Build BST
; =============================

buildtree:
	push 0x0 			; i = 0
	save *i

.loop:
	call func_insert 	; insert()

	push *i
	inc
	save *i 			; i++

	push *i
	push *n
	cmp2

	jnz .loop			; if (i != n) then continue else break

; =============================
; Print Tree
; =============================

printtree:
	times 0x3 push 0x7FFFFFFF
	times 0x3 print				; just some debug info

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
			jgz ..end 			; if (is stack empty) then break

			save *cur 			; pop value from stack
			
			push [arr], *cur
			print  				; print arr[cur]

		..right:
			push [right], *cur
			save *cur 			; (current element) = (right child)

			jmp .loop  			; continue
		..end:

; =============================
; Program end
; =============================
prog_end:

	
	[section _data, Data]
i:		dd 0x0 		; counter
n:		dd 0x20 	; amount of numbers

cur:	dd 0x0			; index of element to operate with
amount: dd 0xFFFFFFFF 	; index of element to add

arr:	times 0x20 dd 0x0 			; values
left:	times 0x20 dd 0xFFFFFFFF	; pointer for left child 
right:	times 0x20 dd 0xFFFFFFFF	; pointer for right child
exist:	times 0x20 dd 0x0 			; is binary tree cell exist (is not NULL ?)

