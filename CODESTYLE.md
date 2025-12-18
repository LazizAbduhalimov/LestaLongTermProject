# C# Code Style Guidelines

- Use `var` when the type is obvious from the right-hand side or context (e.g., `new`, LINQ, literals, method returns with clear names). Avoid `var` when it harms readability.
- Prefer expression-bodied members for simple getters or one-liners when it improves clarity.
- Use PascalCase for public properties, methods, classes; camelCase for private fields and locals.
- Private fields: prefix with underscore (e.g., `_phase`).
- Avoid magic numbers; expose tunable values via serialized fields when relevant to gameplay.
- Keep MonoBehaviour `Update` light; push heavy logic to methods or systems.
- Use `readonly` for fields that are set once (e.g., cached references) when possible.
- Always check for `null` when using optional references from the Inspector.
- Prefer `Time.deltaTime` for frame-based movement; use `FixedUpdate` only for physics-driven motion.
- Keep namespaces consistent with folder structure if/when namespaces are introduced.
