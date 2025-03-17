function Lab1()
    eps = 0.01;
    roots = [];
    root_intervals = [];

    func = @(x) sin(2 * x) + cos(x .^ 3);

    checkFunc(func, -5, 5, 0);

    fprintf('\n%d розвязків:\n', length(roots));
    for i = 1:length(roots)
        fprintf('x = %.2f\n', roots(i));
    end
end

function checkFunc(func, left, right, depth)
    if depth > 2
        FindRoot([left, right], func);
        return;
    end

    count = 10;
    h = abs(right - left) / count;

    for i = 0:count-1
        a = left + i * h;
        b = left + (i + 1) * h;

        if sign(func(a)) ~= sign(func(b))
            checkDerivative(func, a, b, depth, true);
        else
            checkDerivative(func, a, b, depth, false);
        end
    end
end

function checkDerivative(func, left, right, depth, signChanged)
    h = abs(right - left) / 5;
    sameSign = true;
    prevDeriv = dfx(func, left);

    for i = 1:5
        x = left + i * h;
        derivative = dfx(func, x);

        if sign(prevDeriv) ~= sign(derivative)
            sameSign = false;
            checkFunc(func, left, right, depth + 1);
        end

        prevDeriv = derivative;
    end

    if sameSign && signChanged
        FindRoot([left, right], func);
    end
end

function derivative = dfx(func, x)
    h = 0.01;
    derivative = (func(x + h) - func(x)) / h;
end

function FindRoot(interval, func)
    x0 = interval(1);
    x1 = interval(2);
    f0 = func(x0);
    f1 = func(x1);

    if abs(f0) < eps
        AddRoot(x0, f0);
        return;
    end

    if abs(f1) < eps
        AddRoot(x1, f1);
        return;
    end

    count = 0;
    maxCount = 4;

    do
        x2 = x1 - f1 * (x1 - x0) / (f1 - f0);

        if x2 < min(x0, x1) || x2 > max(x0, x1)
            x2 = (x0 + x1) / 2;
        end

        f2 = func(x2);

        if abs(f2) < eps || abs(x2 - x1) < eps
            AddRoot(x2, f2);
            return;
        end

        x0 = x1; f0 = f1;
        x1 = x2; f1 = f2;
        count = count + 1;
    until count >= maxCount
end

function AddRoot(root, funcValue)
    global roots eps;

    if isempty(roots) || all(abs(roots - root) >= eps)
        roots = [roots, root];
        fprintf("Розв'язок: x = %.3f, f(x) = %.3f\n", root, funcValue);
    end
end

Lab1();